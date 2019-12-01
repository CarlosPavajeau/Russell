using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class TransportFormRepository : Repository, ISave<TransportForm>, ISearch<TransportForm>, IUpdate
    {
        static readonly string[] TRANSPORT_FORM_FIELDS = { "@transport_form_number", "@start_date", "@depature_time",
                                                           "@value_of_tickets", "@total_value", "@license_plate", "@route_code", 
                                                           "@dispatcher"};

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

                MapCommandParameters(command, TRANSPORT_FORM_FIELDS,
                    new object[]
                    {
                        data.Number,
                        data.Date,
                        data.DepartureTime,
                        data.ValueOfTickets,
                        data.TotalValue,
                        data.Vehicle.LicensePlate,
                        data.Route.Code,
                        data.Dispatcher.ID
                    });

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
