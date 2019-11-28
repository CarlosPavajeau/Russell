using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class CommendRepository : DeliveryRepository, ISave<Commend>, ISearch<Commend>
    {
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

                command.Parameters.Add(CreateDbParameter(command, "@delivery_number", data.Number));
                command.Parameters.Add(CreateDbParameter(command, "@freight_value", data.FreightValue));
                command.Parameters.Add(CreateDbParameter(command, "@agreement", data.Agreement));
                command.Parameters.Add(CreateDbParameter(command, "@description", data.Description));
                command.Parameters.Add(CreateDbParameter(command, "@license_plate", data.Vehicle.LicensePlate));

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
