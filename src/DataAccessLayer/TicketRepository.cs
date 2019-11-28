using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class TicketRepository : Repository, ISave<Ticket>, ISearch<Ticket>, IUpdate, IDelete
    {
        public TicketRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(Ticket data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO tickets(ticket_number, seats, ticket_date, total, transport_form_number, passenger) " +
                                      "VALUES(@ticket_number, @seats, @ticket_date, @total, @transport_form_number, @passenger)";

                command.Parameters.Add(CreateDbParameter(command, "@ticket_number", data.Number));
                command.Parameters.Add(CreateDbParameter(command, "@seats", data.Seats));
                command.Parameters.Add(CreateDbParameter(command, "@ticket_date", data.Date));
                command.Parameters.Add(CreateDbParameter(command, "@total", data.Total));
                command.Parameters.Add(CreateDbParameter(command, "@transport_form_number", data.GetTransportFormCode()));
                command.Parameters.Add(CreateDbParameter(command, "@passenger", data.Passenger.ID));

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
