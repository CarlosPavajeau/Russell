using System;
using System.Collections.Generic;
using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class CommendRepository : DeliveryRepository, ISave<Commend>, ISearch<Commend>, IMap<Commend>, IGetAllData<Commend>
    {
        static readonly string[] COMMEND_FIELDS = { "@delivery_number", "@freight_value", "@agreement", "@description", "@license_plate" };

        public CommendRepository(DbConnection connection) : base(connection)
        {
            
        }

        public bool Save(Commend data)
        {
            if (!base.Save(data))
                return false;

            using (var command = CreateCommand())
            {
                command.CommandText = "INSERT INTO commends(delivery_number, freight_value, agreement, description, license_plate)" +
                                      "VALUES(@delivery_number, @freight_value, @agreement, @description, @license_plate)";

                MapCommandParameters(command, COMMEND_FIELDS,
                    new object[]
                    {
                        data.Number,
                        data.FreightValue,
                        data.Agreement,
                        data.Description,
                        data.Vehicle.LicensePlate
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public Commend Search(string primaryKey)
        {
            using (var command = CreateCommand())
            {
                command.CommandText = "SELECT dl.delivery_number, dl.delivery_date, dl.destination, dl.state, dl.sender, " +
                                      "dl.receiver, dl.dispatcher, cm.license_plate, cm.freight_value, cm.agreement, cm.description " +
                                      "FROM commends cm " +
                                      "JOIN deliveries dl ON cm.delivery_number = dl.delivery_number" +
                                      "WHERE cm.delivery_number = @delivery_number";

                command.Parameters.Add(CreateDbParameter(command, "@delivery_number", primaryKey));

                using (var dbDataReader = command.ExecuteReader())
                    return dbDataReader.Read() ? Map(dbDataReader) : null;
            }
        }

        public Commend Map(DbDataReader dbDataReader)
        {
            Person sender, receiver;
            PersonRepository personRepository = new PersonRepository(dbConnection);

            sender = personRepository.Search(dbDataReader.GetString(4));
            receiver = personRepository.Search(dbDataReader.GetString(5));

            if (sender is null || receiver is null)
                return null;

            AdministrativeEmployeeRepository administrativeEmployee = new AdministrativeEmployeeRepository(dbConnection);
            VehicleRepository vehicleRepository = new VehicleRepository(dbConnection);
            AdministrativeEmployee dispatcher = administrativeEmployee.Search(dbDataReader.GetString(6), true);
            Vehicle vehicle = vehicleRepository.Search(dbDataReader.GetString(7));

            if (dispatcher is null || vehicle is null)
                return null;

            string delivery_number, destination, description;
            DateTime delivery_date;
            decimal freightValue, agreement;
            State state;

            delivery_number = dbDataReader.GetString(0);
            delivery_date = dbDataReader.GetDateTime(1);
            description = dbDataReader.GetString(10);
            destination = dbDataReader.GetString(2);

            freightValue = dbDataReader.GetDecimal(8);
            agreement = dbDataReader.GetDecimal(9);

            string stateStr = dbDataReader.GetString(3);
            state = (stateStr == "D") ? State.Delivered : (stateStr == "C") ? State.Cancel : State.Active;

            return new Commend(delivery_number, delivery_date, sender, receiver, dispatcher, destination, description, freightValue, agreement, vehicle, state);
        }

        public IList<Commend> GetAllData()
        {
            IList<Commend> commends = new List<Commend>();

            using (var command = CreateCommand())
            {
                command.CommandText = "SELECT dl.delivery_number, dl.delivery_date, dl.destination, dl.state, dl.sender, " +
                                      "dl.receiver, dl.dispatcher, cm.license_plate, cm.freight_value, cm.agreement, cm.description " +
                                      "FROM commends cm " +
                                      "JOIN deliveries dl ON cm.delivery_number = dl.delivery_number";

                using (var dbDataReader = command.ExecuteReader())
                {
                    while (dbDataReader.Read())
                        commends.Add(Map(dbDataReader));
                }
            }

            return commends;
        }
    }
}
