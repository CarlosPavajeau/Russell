using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class VehicleRepository : Repository, ISave<Vehicle>, ISearch<Vehicle>, IUpdate, IDelete
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
                return null;
            }
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
