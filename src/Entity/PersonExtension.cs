using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public static class PersonExtension
    {
        public static Passenger ToPassenger(this Person person)
        {
            return new Passenger(person.ID, person.FirstName, person.SecondName, person.LastName, person.SecondLastName);
        }
    }
}
