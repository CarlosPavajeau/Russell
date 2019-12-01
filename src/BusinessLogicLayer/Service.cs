using System.Data.Common;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public abstract class Service
    {
        const string CONNECTION_STR = @"Data Source=DESKTOP-ND2GLJC\SQLEXPRESS;Initial Catalog=db_cootransnevada;Integrated Security=True";

        protected DbConnection dbConnection;

        public Service()
        {
            dbConnection = new SqlConnection(CONNECTION_STR);
        }
    }
}
