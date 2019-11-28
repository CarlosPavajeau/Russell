using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public abstract class DeliveryRepository : Repository, ISave<Delivery>, IUpdate
    {
        public DeliveryRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(Delivery data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO deliveries(delivery_number, destination, delivery_date, state, dispacher, sender, receiver)" +
                                      "VALUES(@delivery_number, @destination, @delivery_date, @state, @dispacher, @sender, @receiver)";

                command.Parameters.Add(CreateDbParameter(command, "@delivery_number", data.Number));
                command.Parameters.Add(CreateDbParameter(command, "@destination", data.Destination));
                command.Parameters.Add(CreateDbParameter(command, "@delivery_date", data.Date));
                command.Parameters.Add(CreateDbParameter(command, "@state", data.State.ToString()[0]));
                command.Parameters.Add(CreateDbParameter(command, "@dispatcher", data.Dispatcher.ID));
                command.Parameters.Add(CreateDbParameter(command, "@sender", data.Sender.ID));
                command.Parameters.Add(CreateDbParameter(command, "@receiver", data.Receiver.ID));

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
