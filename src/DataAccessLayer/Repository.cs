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

        protected DbParameter CreateDbParameter(DbCommand command, string parameterName, object value)
        {
            DbParameter dbParameter = command.CreateParameter();
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;

            return dbParameter;
        }
    }
}
