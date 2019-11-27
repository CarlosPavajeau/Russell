using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class PersonRepository : Repository, ISave<Person>, ISearch<Person>, IUpdate, IDelete
    {
        public PersonRepository(DbConnection connection) : base(connection)
        {
        }

        public bool Save(Person data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO people(person_id, first_name, second_name, last_name, second_last_name) " +
                                      "VALUES(@person_id, @first_name, @second_name, @last_name, @second_last_name)";

                command.Parameters.Add(CreateDbParameter(command, "@person_id", data.ID));
                command.Parameters.Add(CreateDbParameter(command, "@first_name", data.FirstName));
                command.Parameters.Add(CreateDbParameter(command, "@second_name", data.SecondName));
                command.Parameters.Add(CreateDbParameter(command, "@last_name", data.LastName));
                command.Parameters.Add(CreateDbParameter(command, "@second_last_name", data.SecondLastName));

                command.ExecuteNonQuery();
                return true;
            }
        }

        public Person Search(string primaryKey)
        {
            return null;
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = $"UPDATE people SET { columToModify } = @newValue WHERE person_id = @primaryKey";

                command.Parameters.Add(CreateDbParameter(command, "@newValue", newValue));
                command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primarykey));

                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(string primaryKey)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "DELETE people WHERE person_id = @primaryKey";

                command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primaryKey));

                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
