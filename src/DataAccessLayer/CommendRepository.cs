using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class CommendRepository : DeliveryRepository, ISave<Commend>, ISearch<Commend>
    {
        static readonly string[] COMMEND_FIELDS = { "@delivery_number", "@freight_value", "@agreement", "@description", "@license_plate" };

        public CommendRepository(DbConnection connection) : base(connection)
        {
            
        }

        public bool Save(Commend data)
        {
            if (!base.Save(data))
                return false;

            using (var command = dbConnection.CreateCommand())
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
            return null;
        }
    }
}
