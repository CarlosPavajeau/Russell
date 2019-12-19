using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public abstract class DeliveryRepository : Repository, ISave<Delivery>, IUpdate, ICount
    {
        static readonly string[] DELIVERY_FIELDS = { "@delivery_number", "@destination", "@delivery_date", "@state", 
                                                     "@dispatcher", "@sender", "@receiver" };

        public DeliveryRepository(DbConnection connection) : base(connection)
        {

        }

        public int Count
        {
            get
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM deliveries";
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Save(Delivery data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO deliveries(delivery_number, destination, delivery_date, state, dispatcher, sender, receiver)" +
                                      "VALUES(@delivery_number, @destination, @delivery_date, @state, @dispatcher, @sender, @receiver)";

                MapCommandParameters(command, DELIVERY_FIELDS,
                    new object[]
                    {
                        data.Number,
                        data.Destination,
                        data.Date,
                        data.State.ToString()[0],
                        data.Dispatcher.ID,
                        data.Sender.ID,
                        data.Receiver.ID
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = $"UPDATE deliveries SET {columToModify} = @newValue WHERE delivery_number = @primaryKey";

                command.Parameters.Add(CreateDbParameter(command, "@newValue", newValue));
                command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primarykey));

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
