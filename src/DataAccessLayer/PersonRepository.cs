using Entity;
using System.Data.Common;

namespace DataAccessLayer
{
    public class PersonRepository : Repository, ISave<Person>, ISearch<Person>, IUpdate, IDelete
    {
        static readonly string[] PERSON_FIELDS = { "@person_id", "@first_name", "@last_name", "@second_last_name" };

        public PersonRepository(DbConnection connection) : base(connection)
        {
        }

        public bool Save(Person data)
        {
            using (var command = dbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO people(person_id, first_name, second_name, last_name, second_last_name) " +
                                      "VALUES(@person_id, @first_name, @second_name, @last_name, @second_last_name)";

                MapCommandParameters(command, PERSON_FIELDS,
                    new object[] {
                        data.ID,
                        data.FirstName,
                        data.SecondName,
                        data.LastName,
                        data.SecondLastName
                    });

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
