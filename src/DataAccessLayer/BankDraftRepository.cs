using System;
using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class BankDraftRepository : DeliveryRepository, ISave<BankDraft>, ISearch<BankDraft>, IMap<BankDraft>
    {
        static readonly string[] BANKDRAFT_FIELDS = { "@delivery_number", "@value_to_send", "@cost" };
        public BankDraftRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(BankDraft data)
        {
            if (!base.Save(data))
                return false;

            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO bankdrafts(delivery_number, value_to_send, cost)" +
                                      "VALUES(@delivery_number, @value_to_send, @cost)";

                MapCommandParameters(command, BANKDRAFT_FIELDS,
                    new object[]
                    {
                        data.Number,
                        data.ValueToSend,
                        data.Cost
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public BankDraft Search(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT dl.delivery_number, dl.delivery_date, dl.destination, dl.state, dl.sender, dl.receiver, " +
                                      "dl.dispatcher, bd.value_to_send, bd.cost " +
                                      "FROM bankdrafts bd " +
                                      "JOIN deliveries dl ON bd.delivery_number = dl.delivery_number" +
                                      "WHERE bd.delivery_number = @delivery_number";

                command.Parameters.Add(CreateDbParameter(command, "@delivery_number", primaryKey));

                using (var dbDataReader = command.ExecuteReader())
                    return Map(dbDataReader);
            }
        }

        public BankDraft Map(DbDataReader dbDataReader)
        {
            if (!dbDataReader.Read())
                return null;

            Person sender, receiver;
            PersonRepository personRepository = new PersonRepository(dbConnection);

            sender = personRepository.Search(dbDataReader.GetString(4));
            receiver = personRepository.Search(dbDataReader.GetString(5));

            if (sender is null || receiver is null)
                return null;

            AdministrativeEmployeeRepository administrativeEmployeeRepository = new AdministrativeEmployeeRepository(dbConnection);
            AdministrativeEmployee dispatcher = administrativeEmployeeRepository.Search(dbDataReader.GetString(6));

            if (dispatcher is null)
                return null;

            string delivery_number, destination;
            DateTime delivery_date;
            decimal valueToSend, cost;
            State state;

            delivery_number = dbDataReader.GetString(0);
            delivery_date = dbDataReader.GetDateTime(1);
            destination = dbDataReader.GetString(2);

            valueToSend = dbDataReader.GetDecimal(7);
            cost = dbDataReader.GetDecimal(8);

            string stateStr = dbDataReader.GetString(3);
            state = (stateStr == "D") ? State.DELIVERED : (stateStr == "C") ? State.CANCEL : State.ACTIVE;

            return new BankDraft(delivery_number, delivery_date, sender, receiver, dispatcher, destination, valueToSend, cost, state);
        }

    }
}
