using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class EmployeeRepository : PersonRepository, ISave<Employee>, ISearch<Employee>
    {
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

                command.Parameters.Add(CreateDbParameter(command, "@person_id", data.ID));
                command.Parameters.Add(CreateDbParameter(command, "@cellphone", data.Cellphone));
                command.Parameters.Add(CreateDbParameter(command, "@email", data.Email));
                command.Parameters.Add(CreateDbParameter(command, "@address", data.Address));

                command.ExecuteNonQuery();
                return true;
            }
        }

        Employee ISearch<Employee>.Search(string primaryKey)
        {
            return null;
        }
    }
}
