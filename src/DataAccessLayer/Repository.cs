using System;
using System.Data.Common;

namespace DataAccessLayer
{
    public abstract class Repository
    {
        protected DbConnection dbConnection;

        protected Repository(DbConnection connection)
        {
            dbConnection = connection;
        }
    }
}
