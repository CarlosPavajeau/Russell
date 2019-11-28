using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class TransportFormRepository : Repository, ISave<TransportForm>, ISearch<TransportForm>, IUpdate
    {
        private readonly FinalcialInformationRepository _finalcialInformationRepository;
        public TransportFormRepository(DbConnection connection) : base(connection)
        {
            _finalcialInformationRepository = new FinalcialInformationRepository(connection);
        }

        public bool Save(TransportForm data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO transport_forms(transport_form_number, start_date, depature_time, " +
                                      "value_of_tickets, total_value, license_plate, route_code, dispatcher) " +
                                      "VALUES(@transport_form_number, @start_date, @depature_time, " +
                                             "@value_of_tickets, @total_value, @license_plate, @route_code, @dispatcher)";

                command.Parameters.Add(CreateDbParameter(command, "@transport_form_number", data.Number));
                command.Parameters.Add(CreateDbParameter(command, "@start_date", data.Date));
                command.Parameters.Add(CreateDbParameter(command, "@depature_time", data.DepartureTime));
                command.Parameters.Add(CreateDbParameter(command, "@value_of_tickets", data.ValueOfTickets));
                command.Parameters.Add(CreateDbParameter(command, "@total_value", data.TotalValue));
                command.Parameters.Add(CreateDbParameter(command, "@license_plate", data.Vehicle.LicensePlate));
                command.Parameters.Add(CreateDbParameter(command, "@route_code", data.Route.Code));
                command.Parameters.Add(CreateDbParameter(command, "@dispatcher", data.Dispatcher.ID));

                command.ExecuteNonQuery();
                return true;
            }
        }

        public TransportForm Search(string primaryKey)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            throw new System.NotImplementedException();
        }

        class FinalcialInformationRepository : Repository, IUpdate
        {
            public FinalcialInformationRepository(DbConnection connection) : base(connection)
            {

            }

            public bool Update(string primarykey, string columToModify, object newValue)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
