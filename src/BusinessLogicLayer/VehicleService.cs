using DataAccessLayer;
using Entity;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class VehicleService : Service, ISave<Vehicle>, ISearch<Vehicle>, IUpdate, IDelete, IGetAllData<Vehicle>
    {
        private readonly VehicleRepository _vehicleRepository;

        public VehicleService()
        {
            _vehicleRepository = new VehicleRepository(dbConnection);
        }

        public bool Save(Vehicle data)
        {
            try
            {
                dbConnection.Open();
                return _vehicleRepository.Save(data);
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

        public Vehicle Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _vehicleRepository.Search(primaryKey);
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
                return _vehicleRepository.Update(primarykey, columToModify, newValue);
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
                return _vehicleRepository.Delete(primaryKey);
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

        public IList<Vehicle> GetAllData()
        {
            try
            {
                dbConnection.Open();
                return _vehicleRepository.GetAllData();
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
