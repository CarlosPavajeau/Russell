using Entity;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccessLayer
{
    public class PersonRepository : Repository, ISave<Person>, ISearch<Person>, IUpdate, IDelete, IMap<Person>, IGetAllData<Person>
    {
        static readonly string[] PERSON_FIELDS = { "@person_id", "@first_name", "@second_name", "@last_name", "@second_last_name" };

        public PersonRepository(DbConnection connection) : base(connection)
        {
        }

        public bool Save(Person data)
        {
            using DbCommand command = CreateCommand();
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

        public Person Search(string primaryKey)
        {
            using DbCommand command = CreateCommand();
            command.CommandText = "SELECT person_id, first_name, second_name, last_name, second_last_name " +
                                  "FROM people WHERE person_id = @person_id";

            command.Parameters.Add(CreateDbParameter(command, "@person_id", primaryKey));

            using DbDataReader dbDataReader = command.ExecuteReader();
            return dbDataReader.Read() ? Map(dbDataReader) : null;
        }

        public Person Map(DbDataReader dbDataReader)
        {
            string person_id, first_name, second_name, last_name, second_last_name;

            person_id = dbDataReader.GetString(0);
            first_name = dbDataReader.GetString(1);
            second_name = dbDataReader.GetString(2);
            last_name = dbDataReader.GetString(3);
            second_last_name = dbDataReader.GetString(4);

            return new Person(person_id, first_name, second_name, last_name, second_last_name);
        }

        public bool Update(string primarykey, string columToModify, object newValue)
        {
            using DbCommand command = CreateCommand();
            command.CommandText = $"UPDATE people SET { columToModify } = @newValue WHERE person_id = @primaryKey";

            command.Parameters.Add(CreateDbParameter(command, "@newValue", newValue));
            command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primarykey));

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(string primaryKey)
        {
            using DbCommand command = CreateCommand();
            command.CommandText = "DELETE people WHERE person_id = @primaryKey";

            command.Parameters.Add(CreateDbParameter(command, "@primaryKey", primaryKey));

            return command.ExecuteNonQuery() > 0;
        }

        public IList<Person> GetAllData()
        {
            IList<Person> people = new List<Person>();

            using (DbCommand command = CreateCommand())
            {
                command.CommandText = "SELECT person_id, first_name, second_name, last_name, second_last_name " +
                                      "FROM people";

                using DbDataReader dbDataReader = command.ExecuteReader();
                while (dbDataReader.Read())
                    people.Add(Map(dbDataReader));
            }

            return people;
        }
    }
}
