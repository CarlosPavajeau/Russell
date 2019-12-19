using DataAccessLayer;
using Entity;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class TransportFormService : Service, ISave<TransportForm>, ISave<Ticket>, ISearch<TransportForm>, IUpdate, IGetAllData<TransportForm>, ICount
    {
        private readonly TransportFormRepository _transportFormRepository;

        public TransportFormService()
        {
            _transportFormRepository = new TransportFormRepository(dbConnection);
        }

        public int Count
        {
            get
            {
                try
                {
                    dbConnection.Open();
                    return _transportFormRepository.Count;
                }
                catch (System.Exception)
                {
                    return 0;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }

        public TransportForm CurrentTransportFrom(string dispatcherID)
        {
            try
            {
                dbConnection.Open();
                return _transportFormRepository.CurrentTransportForm(dispatcherID);
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

        public bool UpdateFinancialInformation(string transprotFormCode, FinalcialInformationType type, decimal newValue)
        {
            return Update(transprotFormCode, type.ToString(), newValue);
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
