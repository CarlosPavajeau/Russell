using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class BankDraftRepository : DeliveryRepository, ISave<BankDraft>, ISearch<BankDraft>
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
            return null;
        }
    }
}
