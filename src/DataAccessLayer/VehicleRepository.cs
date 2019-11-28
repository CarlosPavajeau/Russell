using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class VehicleRepository : Repository, ISave<Vehicle>, ISearch<Vehicle>, IUpdate, IDelete
    {
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

                command.Parameters.Add(CreateDbParameter(command, "@license_plate", data.LicensePlate));
                command.Parameters.Add(CreateDbParameter(command, "@internal_number", data.InternalNumber));
                command.Parameters.Add(CreateDbParameter(command, "@property_card_number", data.PropertyCardNumber));
                command.Parameters.Add(CreateDbParameter(command, "@state", data.State));
                command.Parameters.Add(CreateDbParameter(command, "@owner", data.Owner.ID));
                command.Parameters.Add(CreateDbParameter(command, "@driver", data.Driver.ID));

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
            public VehicleFeatureRepository(DbConnection connection) : base(connection)
            {
            }

            public bool Save(Vehicle data)
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "INSET INTO vehicle_features(license_plate, type, mark, model, model_number, color, chairs)" +
                                          "VALUES(@license_plate, @type, @mark, @model, @model_number, @color, @chairs)";

                    command.Parameters.Add(CreateDbParameter(command, "@license_plate", data.LicensePlate));
                    command.Parameters.Add(CreateDbParameter(command, "@type", data.Feature.Type));
                    command.Parameters.Add(CreateDbParameter(command, "@mark", data.Feature.Mark));
                    command.Parameters.Add(CreateDbParameter(command, "@model", data.Feature.Model));
                    command.Parameters.Add(CreateDbParameter(command, "@model_number", data.Feature.ModelNumber));
                    command.Parameters.Add(CreateDbParameter(command, "@color", data.Feature.Color));
                    command.Parameters.Add(CreateDbParameter(command, "@chairs", data.Feature.Chairs));

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
            public ImprintRepository(DbConnection connection) : base(connection)
            {

            }

            public bool Save(Vehicle data)
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "INSET INTO imprints(license_plate, engine_number, chassis_number)" +
                                          "VALUES(@license_plate, @engine_number, @chassis_number)";

                    command.Parameters.Add(CreateDbParameter(command, "@license_plate", data.LicensePlate));
                    command.Parameters.Add(CreateDbParameter(command, "@engine_number", data.Imprint.EngineNumber));
                    command.Parameters.Add(CreateDbParameter(command, "@chassis_number", data.Imprint.ChassisNumber));

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

                        command.Parameters.Add(CreateDbParameter(command, "@license_plate", data.LicensePlate));
                        command.Parameters.Add(CreateDbParameter(command, "@legal_information_type", legalInformation.Key.ToString()));
                        command.Parameters.Add(CreateDbParameter(command, "@due_date", legalInformation.Value.DueDate));
                        command.Parameters.Add(CreateDbParameter(command, "@renovation_date", legalInformation.Value.DateOfRenovation));

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
