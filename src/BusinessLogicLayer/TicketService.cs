using DataAccessLayer;
using Entity;

namespace BusinessLogicLayer
{
    public class TicketService : Service, ISave<Ticket>, ISearch<Ticket>, IUpdate, IDelete
    {
        private readonly TicketRepository _ticketRepository;

        public TicketService()
        {
            _ticketRepository = new TicketRepository(dbConnection);
        }

        public bool Save(Ticket data)
        {
            try
            {
                dbConnection.Open();
                return _ticketRepository.Save(data);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection?.Close();
            }
        }

        public Ticket Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _ticketRepository.Search(primaryKey);
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                dbConnection?.Close();
            }
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            try
            {
                dbConnection.Open();
                return _ticketRepository.Update(primarykey, columToModify, newValue);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection?.Close();
            }
        }

        public bool Delete(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _ticketRepository.Delete(primaryKey);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection?.Close();
            }
        }
    }
}
