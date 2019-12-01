using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class TicketRepository : Repository, ISave<Ticket>, ISearch<Ticket>, IUpdate, IDelete
    {
        static readonly string[] TICKET_FIELDS = { "@ticket_number", "@seats", "@ticket_date", "@total",
                                                   "@transport_form_number", "@passenger"};
        public TicketRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(Ticket data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO tickets(ticket_number, seats, ticket_date, total, transport_form_number, passenger) " +
                                      "VALUES(@ticket_number, @seats, @ticket_date, @total, @transport_form_number, @passenger)";

                MapCommandParameters(command, TICKET_FIELDS,
                    new object[] {
                        data.Number,
                        data.Seats,
                        data.Date,
                        data.Total,
                        data.GetTransportFormCode(),
                        data.Passenger.ID
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public Ticket Search(string primaryKey)
        {
            return null;
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            return false;
        }

        public bool Delete(string primaryKey)
        {
            return false;
        }

    }
}
