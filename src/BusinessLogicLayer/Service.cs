using System.Data.Common;
using System.Data.SqlClient;

namespace BusinessLogicLayer
{
    public abstract class Service
    {
        const string CONNECTION_SQL_STR = @"Data Source=DESKTOP-ND2GLJC\SQLEXPRESS;Initial Catalog=db_cootransnevada;Integrated Security=True; MultipleActiveResultSets=true;";
        const string CONNECTION_ORACLE_STR = @"User Id=cootransnevada; password=cootransnevada; Data Source=localhost:1521/xepdb1; Pooling=false;";

        protected DbConnection dbConnection;

        public Service()
        {
            dbConnection = new SqlConnection(CONNECTION_SQL_STR);
        }
    }
}
