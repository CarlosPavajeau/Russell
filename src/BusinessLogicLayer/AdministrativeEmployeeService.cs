using DataAccessLayer;
using Entity;

namespace BusinessLogicLayer
{
    public class AdministrativeEmployeeService : Service, ISave<AdministrativeEmployee>, ISearch<AdministrativeEmployee>, IUpdate, IDelete
    {
        AdministrativeEmployeeRepository _administrativeEmployeeRepository;

        public AdministrativeEmployeeService()
        {
            _administrativeEmployeeRepository = new AdministrativeEmployeeRepository(dbConnection);
        }

        public bool Save(AdministrativeEmployee data)
        {
            try
            {
                dbConnection.Open();
                return _administrativeEmployeeRepository.Save(data);
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

        public AdministrativeEmployee Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _administrativeEmployeeRepository.Search(primaryKey);
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
                return _administrativeEmployeeRepository.Update(primarykey, columToModify, newValue);
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
                return _administrativeEmployeeRepository.Delete(primaryKey);
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

        public bool IsEmpty()
        {
            try
            {
                dbConnection.Open();
                return _administrativeEmployeeRepository.IsEmpty();
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
