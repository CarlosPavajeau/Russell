using DataAccessLayer;
using Entity;

namespace BusinessLogicLayer
{
    public class RouteService : Service, ISave<Route>, ISearch<Route>, IUpdate, IDelete
    {
        private RouteRepository _routeRepository;

        public RouteService()
        {
            _routeRepository = new RouteRepository(dbConnection);
        }

        public bool Save(Route data)
        {
            try
            {
                dbConnection.Open();
                return _routeRepository.Save(data);
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

        public Route Search(string primaryKey)
        {
            try
            {
                dbConnection.Open();
                return _routeRepository.Search(primaryKey);
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
                return _routeRepository.Update(primarykey, columToModify, newValue);
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
                return _routeRepository.Delete(primaryKey);
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
