using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class EmployeeRepository : PersonRepository, ISave<Employee>, ISearch<Employee>, IMap<Employee>
    {
        static readonly string[] EMPLOYEE_FIELDS = { "@person_id", "@cellphone", "@email", "@address" };

        public EmployeeRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(Employee data)
        {
            if (!base.Save(data))
                return false;

            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO employees(person_id, cellphone, email, address) " +
                                      "VALUES(@person_id, @cellphone, @email, @address)";

                MapCommandParameters(command, EMPLOYEE_FIELDS,
                    new object[]
                    {
                        data.ID,
                        data.Cellphone,
                        data.Email,
                        data.Address
                    });

                command.ExecuteNonQuery();
                return true;
            }
        }

        public new Employee Search(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "SELECT em.person_id, p.first_name, p.second_name, p.last_name, p.second_last_name, em.cellphone, " +
                                      "em.email, em.address FROM employees em " +
                                      "JOIN people p ON em.person_id = p.person_id" +
                                      "WHERE em.person_id = @person_id";

                command.Parameters.Add(CreateDbParameter(command, "@person_id", primaryKey));

                return Map(command.ExecuteReader());
            }
        }

        public new Employee Map(DbDataReader dbDataReader)
        {
            if (!dbDataReader.Read())
                return null;

            string person_id, first_name, second_name, last_name, second_last_name,
                   cellphone, email, address;

            person_id = dbDataReader.GetString(0);
            first_name = dbDataReader.GetString(1);
            second_name = dbDataReader.GetString(2);
            last_name = dbDataReader.GetString(3);
            second_last_name = dbDataReader.GetString(4);
            cellphone = dbDataReader.GetString(5);
            email = dbDataReader.GetString(6);
            address = dbDataReader.GetString(7);

            return new Employee(person_id, first_name, second_name, last_name, second_last_name, cellphone, email, address);
        }
    }
}
