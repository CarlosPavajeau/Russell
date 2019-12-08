using DataAccessLayer;
using Entity;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class PersonService : Service, ISave<Person>, ISearch<Person>, IUpdate, IDelete, IGetAllData<Person>
    {
        private readonly PersonRepository _personRepository;

        public PersonService()
        {
            _personRepository = new PersonRepository(dbConnection);
        }

        public bool Save(Person data)
        {
            try
            {
                dbConnection.Open();
                return _personRepository.Save(data);
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

        public Person Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _personRepository.Search(primaryKey);
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
                return _personRepository.Update(primarykey, columToModify, newValue);
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
                return _personRepository.Delete(primaryKey);
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

        public IList<Person> GetAllData()
        {
            try
            {
                dbConnection.Open();
                return _personRepository.GetAllData();
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