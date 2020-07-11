using Common.Settings;
using System.Data.Common;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public abstract class Service
    {
        protected DbConnection dbConnection;

        public Service()
        {
            dbConnection = new SqlConnection(@GeneralSettings.ConnectionString);
        }
    }
}
