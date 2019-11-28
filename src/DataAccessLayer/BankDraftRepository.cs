using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class BankDraftRepository : DeliveryRepository, ISave<BankDraft>, ISearch<BankDraft>
    {
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

                command.Parameters.Add(CreateDbParameter(command, "@delivery_number", data.Number));
                command.Parameters.Add(CreateDbParameter(command, "@value_to_send", data.ValueToSend));
                command.Parameters.Add(CreateDbParameter(command, "@cost", data.Cost));

                command.ExecuteNonQuery();
                return true;
            }
        }

        public BankDraft Search(string primaryKey)
        {
            return null;
        }
    }
}
