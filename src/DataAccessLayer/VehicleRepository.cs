using System;
using System.Collections.Generic;
using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class VehicleRepository : Repository, ISave<Vehicle>, ISearch<Vehicle>, IUpdate, IDelete, IMap<Vehicle>
    {
        static readonly string[] VEHICLE_FIELDS = { "@license_plate", "@internal_number", "@property_card_number", "@state",
                                                    "@owner", "@driver"};

        private readonly VehicleFeatureRepository _featureRepository;
        private readonly ImprintRepository _imprintRepository;
        private readonly LegalInformationRepository _legalInformationRepository;

        public VehicleRepository(DbConnection connection) : base(connection)
        {
            _featureRepository = new VehicleFeatureRepository(connection);
            _imprintRepository = new ImprintRepository(connection);
            _legalInformationRepository = new LegalInformationRepository(connection);
        }

        public bool Save(Vehicle data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO vehicles(license_plate, internal_number, property_card_number, state, owner, driver)" +
                                      "VALUES(@license_plate, @internal_number, @property_card_number, @state, @owner, @driver)";

                MapCommandParameters(command, VEHICLE_FIELDS,
                    new object[]
                    {
                        data.LicensePlate,
                        data.InternalNumber,
                        data.PropertyCardNumber,
                        data.State,
                        data.Owner.ID,
                        data.Driver.ID
                    });

                command.ExecuteNonQuery();

                _featureRepository.Save(data);
                _imprintRepository.Save(data);
                _legalInformationRepository.Save(data);

                return true;
            }
        }

        public Vehicle Search(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT vh.license_plate, vh.internal_number, vh.property_card_number, vh.driver, vh.owner, vh.state, vhf.type, " +
                                      "vhf.mark, vhf.model, vhf.model_number, vhf.color, vhf.chairs, im.engine_number, im.chassis_number " +
                                      "FROM vehicles vh " +
                                      "JOIN vehicle_features vhf ON vh.license_plate = vhf.license_plate " +
                                      "JOIN imprints im ON vh.license_plate = im.license_plate" +
                                      "WHERE vh.license_plate = @license_plate";

                command.Parameters.Add(CreateDbParameter(command, "@license_plate", primaryKey));

                return Map(command.ExecuteReader());
            }
        }

        public Vehicle Map(DbDataReader dbDataReader)
        {
            if (!dbDataReader.Read())
                return null;

            EmployeeRepository employeeRepository = new EmployeeRepository(dbConnection);
            Employee driver, owner;
            driver = employeeRepository.Search(dbDataReader.GetString(3));
            owner = employeeRepository.Search(dbDataReader.GetString(4));

            if (driver is null || owner is null)
                return null;

            string licensePlate, internalNumber, propertyCardNumber;

            licensePlate = dbDataReader.GetString(0);
            internalNumber = dbDataReader.GetString(1);
            propertyCardNumber = dbDataReader.GetString(2);

            Vehicle vehicle = new Vehicle(licensePlate, internalNumber, propertyCardNumber, owner, driver);

            vehicle.Feature.Type = dbDataReader.GetString(5);
            vehicle.Feature.Mark = dbDataReader.GetString(6);
            vehicle.Feature.Model = dbDataReader.GetString(7);
            vehicle.Feature.ModelNumber = dbDataReader.GetString(8);
            vehicle.Feature.Color = dbDataReader.GetString(9);
            vehicle.Feature.Chairs = dbDataReader.GetByte(10);

            vehicle.Imprint.EngineNumber = dbDataReader.GetString(11);
            vehicle.Imprint.ChassisNumber = dbDataReader.GetString(12);

            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT legal_information_type, due_date, renovation_date " +
                                      "FROM legal_information WHERE license_plate = @license_plate ORDER BY legal_information_type";

                command.Parameters.Add(CreateDbParameter(command, "@license_plate", vehicle.LicensePlate));

                DbDataReader legalInformationDR = command.ExecuteReader();

                if (!legalInformationDR.Read())
                    return null;

                DateTime renovationDate, dueDate;
                dueDate = legalInformationDR.GetDateTime(1);
                renovationDate = legalInformationDR.GetDateTime(2);

                vehicle.AddLegalInformation(LegalInformationType.BIMONTHLYREVIEW, dueDate, renovationDate);
                legalInformationDR.NextResult();

                dueDate = legalInformationDR.GetDateTime(1);
                renovationDate = legalInformationDR.GetDateTime(2);
                vehicle.AddLegalInformation(LegalInformationType.LICENSE, dueDate, renovationDate);
                legalInformationDR.NextResult();

                dueDate = legalInformationDR.GetDateTime(1);
                renovationDate = legalInformationDR.GetDateTime(2);
                vehicle.AddLegalInformation(LegalInformationType.OPERATIONCARD, dueDate, renovationDate);
                legalInformationDR.NextResult();

                dueDate = legalInformationDR.GetDateTime(1);
                renovationDate = legalInformationDR.GetDateTime(2);
                vehicle.AddLegalInformation(LegalInformationType.SOAT, dueDate, renovationDate);
                legalInformationDR.NextResult();

                dueDate = legalInformationDR.GetDateTime(1);
                renovationDate = legalInformationDR.GetDateTime(2);
                vehicle.AddLegalInformation(LegalInformationType.TECHNOMECHANICALREVIEW, dueDate, renovationDate);

            }

            return vehicle;
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            using (var command = dbConnection.CreateCommand())
            {
                return true;
            }
        }

        public bool Delete(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                return true;
            }
        }

        class VehicleFeatureRepository : Repository, ISave<Vehicle>, IUpdate
        {
            static readonly string[] VEHICLE_FEATURE_FIELDS = { "@license_plate", "@type", "@mark", "@model", 
                                                                "@model_number", "@color", "@chairs"};

            public VehicleFeatureRepository(DbConnection connection) : base(connection)
            {
            }

            public bool Save(Vehicle data)
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "INSET INTO vehicle_features(license_plate, type, mark, model, model_number, color, chairs)" +
                                          "VALUES(@license_plate, @type, @mark, @model, @model_number, @color, @chairs)";

                    MapCommandParameters(command, VEHICLE_FEATURE_FIELDS,
                        new object[]
                        {
                            data.LicensePlate,
                            data.Feature.Type,
                            data.Feature.Mark,
                            data.Feature.Model,
                            data.Feature.ModelNumber,
                            data.Feature.Color,
                            data.Feature.Chairs
                        });

                    command.ExecuteNonQuery();
                    return true;
                }
            }

            public bool Update(string primarykey, string columToModify, object newValue)
            {
                return false;
            }
        }

        class ImprintRepository : Repository, ISave<Vehicle>, IUpdate
        {
            static readonly string[] IMPRINT_FIELDS = { "@license_plate", "@engine_number", "@chassis_number" };

            public ImprintRepository(DbConnection connection) : base(connection)
            {

            }

            public bool Save(Vehicle data)
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "INSET INTO imprints(license_plate, engine_number, chassis_number)" +
                                          "VALUES(@license_plate, @engine_number, @chassis_number)";

                    MapCommandParameters(command, IMPRINT_FIELDS,
                        new object[]
                        {
                            data.LicensePlate,
                            data.Imprint.EngineNumber,
                            data.Imprint.ChassisNumber
                        });

                    command.ExecuteNonQuery();
                    return true;
                }
            }

            public bool Update(string primarykey, string columToModify, object newValue)
            {
                return false;
            }
        }

        class LegalInformationRepository : Repository, ISave<Vehicle>, IUpdate
        {
            static readonly string[] LEGAL_INFORMATION_FIELDS = { "@license_plate", "@legal_information_type", "@due_date", "@renovation_date" };

            public LegalInformationRepository(DbConnection connection) : base(connection)
            {

            }

            public bool Save(Vehicle data)
            {
                foreach (var legalInformation in data.LegalInformation.GetLegalInformation())
                {
                    using (var command = dbConnection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO legal_information(license_plate, legal_information_type, due_date, renovation_date)" +
                                              "VALUES(@license_plate, @legal_information_type, @due_date, @renovation_date)";

                        MapCommandParameters(command, LEGAL_INFORMATION_FIELDS,
                            new object[]
                            {
                                data.LicensePlate,
                                legalInformation.Key.ToString(),
                                legalInformation.Value.DueDate,
                                legalInformation.Value.DateOfRenovation
                            });

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }

            public bool Update(string primarykey, string columToModify, object newValue)
            {
                return false;
            }
        }

    }
}
