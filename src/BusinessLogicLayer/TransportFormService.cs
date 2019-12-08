using DataAccessLayer;
using Entity;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class TransportFormService : Service, ISave<TransportForm>, ISave<Ticket>, ISearch<TransportForm>, IUpdate, IGetAllData<TransportForm>
    {
        private readonly TransportFormRepository _transportFormRepository;

        public TransportFormService()
        {
            _transportFormRepository = new TransportFormRepository(dbConnection);
        }

        public bool Save(TransportForm data)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Save(data);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public bool Save(Ticket data)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Save(data);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public TransportForm Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Search(primaryKey);
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.Update(primarykey, columToModify, newValue);
            }
            catch (System.Exception)
            {
                return false;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public IList<TransportForm> GetAllData()
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.GetAllData();
            }
            catch (System.Exception)
            {
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
