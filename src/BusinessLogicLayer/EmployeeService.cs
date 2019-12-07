using DataAccessLayer;
using Entity;

namespace BusinessLogicLayer
{
    public class EmployeeService : Service, ISave<Employee>, ISearch<Employee>, IUpdate, IDelete
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository(dbConnection);
        }

        public bool Save(Employee data)
        {
            try
            {
                dbConnection.Open();
                return _employeeRepository.Save(data);
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

        public Employee Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _employeeRepository.Search(primaryKey);
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
                return _employeeRepository.Update(primarykey, columToModify, newValue);
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

        public bool Delete(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _employeeRepository.Delete(primaryKey);
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

    }
}
