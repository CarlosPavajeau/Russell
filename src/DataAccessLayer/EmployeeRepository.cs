using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class EmployeeRepository : PersonRepository, ISave<Employee>, ISearch<Employee>
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
            return null;
        }
    }
}
