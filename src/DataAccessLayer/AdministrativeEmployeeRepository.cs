using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class AdministrativeEmployeeRepository : EmployeeRepository, ISave<AdministrativeEmployee>, ISearch<AdministrativeEmployee>, IMap<AdministrativeEmployee>
    {
        static readonly string[] ADMINISTRATIVE_EMPLOYEE_FIELDS = { "@person_id", "@username", "@passwordname", "@type_user" };
        public AdministrativeEmployeeRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(AdministrativeEmployee data)
        {
            try
            {
                base.Save(data);
            }
            finally
            {
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
                }
            }
            return true;
        }

        public new AdministrativeEmployee Search(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT ad.person_id, pe.first_name, pe.second_name, pe.last_name, pe.second_last_name, " +
                    "em.cellphone, em.email, em.address, ad.username, ad.passwordname, ad.type_user FROM administrative_employees ad " +
                    "JOIN employees em ON em.person_id = ad.person_id JOIN people pe ON pe.person_id = ad.person_id " +
                    "WHERE ad.username = @username";

                command.Parameters.Add(CreateDbParameter(command, "@username", primaryKey));

                using (var dbDataReader = command.ExecuteReader())
                    return (dbDataReader.Read()) ? Map(dbDataReader) : null;
            }
        }

        public AdministrativeEmployee Search(string primaryKey, bool searchByID)
        {
            if (searchByID)
            {
                using (var command = dbConnection.CreateCommand())
                {
                    command.CommandText = "SELECT ad.person_id, pe.first_name, pe.second_name, pe.last_name, pe.second_last_name, " +
                        "em.cellphone, em.email, em.address, ad.username, ad.passwordname, ad.type_user FROM administrative_employees ad " +
                        "JOIN employees em ON em.person_id = ad.person_id JOIN people pe ON pe.person_id = ad.person_id " +
                        "WHERE ad.person_id = @person_id";

                    command.Parameters.Add(CreateDbParameter(command, "@person_id", primaryKey));

                    using (var dbDataReader = command.ExecuteReader())
                        return (dbDataReader.Read()) ? Map(dbDataReader) : null;
                }
            }
            else
                return Search(primaryKey);
        }

        public new AdministrativeEmployee Map(DbDataReader dbDataReader)
        {
            string id, firstName, secondName, lastName, secondLastName,
                   cellphone, email, address, username, passwordname;

            id = dbDataReader.GetString(0);
            firstName = dbDataReader.GetString(1);
            secondName = dbDataReader.GetString(2);
            lastName = dbDataReader.GetString(3);
            secondLastName = dbDataReader.GetString(4);
            cellphone = dbDataReader.GetString(5);
            email = dbDataReader.GetString(6);
            address = dbDataReader.GetString(7);
            username = dbDataReader.GetString(8);
            passwordname = dbDataReader.GetString(9);

            string typeUserStr = dbDataReader.GetString(10);
            TypeUser typeUser = (typeUserStr == "S") ? TypeUser.SUPERUSER : ((typeUserStr == "D") ? TypeUser.DISPATCHER : TypeUser.ADMIN);

            return new AdministrativeEmployee(id, firstName, secondName, lastName, secondLastName, cellphone,
                                              email, address, new User(username, passwordname, typeUser));
        }

        public bool IsEmpty()
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM administrative_employees";

                return ((int)command.ExecuteScalar()) == 0;
            }
        }
    }
}
