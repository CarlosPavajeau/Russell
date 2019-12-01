using System.Data.Common;
using System.Diagnostics;

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

        protected void MapCommandParameters(DbCommand command, string[] parametersNames, object[] values)
        {
            Debug.Assert(parametersNames.Length == values.Length);

            for (int i = 0; i < parametersNames.Length; ++i)
                command.Parameters.Add(CreateDbParameter(command, parametersNames[i], values[i]));
        }
    }
}
