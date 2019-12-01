using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class AdministrativeEmployeeRepository : EmployeeRepository, ISave<AdministrativeEmployee>, ISearch<AdministrativeEmployee>
    {
        static readonly string[] ADMINISTRATIVE_EMPLOYEE_FIELDS = { "@person_id", "@username", "@passwordname", "@type_user" };
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

                MapCommandParameters(command, ADMINISTRATIVE_EMPLOYEE_FIELDS,
                    new object[]
                    {
                        data.ID,
                        data.User.AccessData.User,
                        data.User.AccessData.Password,
                        data.User.TypeUser.ToString()[0]
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public new AdministrativeEmployee Search(string primaryKey)
        {
            return null;
        }
    }
}
