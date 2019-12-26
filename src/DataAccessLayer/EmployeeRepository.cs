using System;
using System.Collections.Generic;
using System.Data.Common;
using Entity;

namespace DataAccessLayer
{
    public class EmployeeRepository : PersonRepository, ISave<Employee>, ISearch<Employee>, IMap<Employee>, IGetAllData<Employee>
    {
        static readonly string[] EMPLOYEE_FIELDS = { "@person_id", "@cellphone", "@email", "@address" };

        public EmployeeRepository(DbConnection connection) : base(connection)
        {

        }

        public bool Save(Employee data)
        {
            try
            {
                base.Save(data);
            }
            catch (Exception)
            {

            }
            finally
            {
                using (var command = CreateCommand())
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
                }
            }
            return true;
        }

        public new Employee Search(string primaryKey)
        {
            using (var command = CreateCommand())
            {
                command.CommandText = "SELECT em.person_id, p.first_name, p.second_name, p.last_name, p.second_last_name, em.cellphone, " +
                                      "em.email, em.address FROM employees em " +
                                      "JOIN people p ON em.person_id = p.person_id " +
                                      "WHERE em.person_id = @person_id";

                command.Parameters.Add(CreateDbParameter(command, "@person_id", primaryKey));

                using (var dbDataReader = command.ExecuteReader())
                    return dbDataReader.Read() ? Map(dbDataReader) : null;
            }
        }

        public new Employee Map(DbDataReader dbDataReader)
        {
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

        public new IList<Employee> GetAllData()
        {
            IList<Employee> employees = new List<Employee>();

            using (var command = CreateCommand())
            {
                command.CommandText = "SELECT em.person_id, p.first_name, p.second_name, p.last_name, p.second_last_name, em.cellphone, " +
                                      "em.email, em.address FROM employees em " +
                                      "JOIN people p ON em.person_id = p.person_id";

                using (var dbDataReader = command.ExecuteReader())
                {
                    while (dbDataReader.Read())
                        employees.Add(Map(dbDataReader));
                }
            }

            return employees;
        }
    }
}
