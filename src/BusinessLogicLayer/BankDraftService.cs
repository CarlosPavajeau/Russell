using DataAccessLayer;
using Entity;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class BankDraftService : Service, ISave<BankDraft>, ISearch<BankDraft>, IUpdate, IGetAllData<BankDraft>
    {
        private readonly BankDraftRepository _bankDraftRepository;

        public BankDraftService()
        {
            _bankDraftRepository = new BankDraftRepository(dbConnection);
        }

        public bool Save(BankDraft data)
        {
            try
            {
                dbConnection.Open();
                return _bankDraftRepository.Save(data);
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

        public BankDraft Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _bankDraftRepository.Search(primaryKey);
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
                return _bankDraftRepository.Update(primarykey, columToModify, newValue);
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

        public IList<BankDraft> GetAllData()
        {
            try
            {
                dbConnection.Open();
                return _bankDraftRepository.GetAllData();
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
