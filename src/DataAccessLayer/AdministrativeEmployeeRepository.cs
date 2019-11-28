using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class AdministrativeEmployeeRepository : EmployeeRepository, ISave<AdministrativeEmployee>, ISearch<AdministrativeEmployee>
    {
        public AdministrativeEmployeeRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(AdministrativeEmployee data)
        {
            if (!base.Save(data))
                return false;

            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO administrative_employees(person_id, username, passwordname, type_user) " +
                                      "VALUES(@person_id, @username, @passwordname, @type_user)";

                command.Parameters.Add(CreateDbParameter(command, "@person_id", data.ID));
                command.Parameters.Add(CreateDbParameter(command, "@username", data.User.AccessData.User));
                command.Parameters.Add(CreateDbParameter(command, "@passwordname", data.User.AccessData.Password));
                command.Parameters.Add(CreateDbParameter(command, "@type_user", data.User.TypeUser.ToString()[0]));

                command.ExecuteNonQuery();
                return true;
            }
        }

        AdministrativeEmployee ISearch<AdministrativeEmployee>.Search(string primaryKey)
        {
            return null;
        }
    }
}
