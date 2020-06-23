using System.Data.Common;
using System.Data.SqlClient;
using Common;

namespace BusinessLogicLayer
{
    public abstract class Service
    {
        const string DB_CONNECTION_CONFIG_FILE = @"db_connection.conf";
        static readonly string CONNECTION_STR = ConfigLoader.LoadDBConnectionString(DB_CONNECTION_CONFIG_FILE);
       
        protected DbConnection dbConnection;

        public Service()
        {
            dbConnection = new SqlConnection(@CONNECTION_STR);
        }
    }
}
