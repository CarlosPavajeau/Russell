using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class Person
    {
        private string id;

        private string firstName;
        private string secondName;
        private string lastName;
        private string secondLastName;

        private string address;
        private string cellphone;
        private string email;

        public Person()
        {

        }

        public Person(string id, string firstName, string secondName, string lastName, 
                      string secondLastName, string address, string cellphone, string email)
        {
            ID = id;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            SecondLastName = secondLastName;
            Address = address;
            Cellphone = cellphone;
            Email = email;
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = (value != string.Empty) ? value : throw new ArgumentException("The id is invalid");
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = (value != string.Empty) ? value : throw new ArgumentException("The first name is invalid");
            }
        }

        public string SecondName
        {
            get
            {
                return secondName;
            }
            set
            {
                secondName = (value != string.Empty) ? value : throw new ArgumentException("The second name is invalid");
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = (value != string.Empty) ? value : throw new ArgumentException("The last name is invalid");
            }
        }

        public string SecondLastName
        {
            get
            {
                return secondLastName;
            }
            set
            {
                secondLastName = (value != string.Empty) ? value : throw new ArgumentException("The second last name is invalid");
            }
        }

        public string Name
        {
            get
            {
                return $"{FirstName} {SecondName} {LastName} {SecondLastName}";
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = (value != string.Empty) ? value : throw new ArgumentException("The address is invalid");
            }
        }

        public string Cellphone
        {
            get
            {
                return cellphone;
            }
            set
            {
                cellphone = (value != string.Empty) ? value : throw new ArgumentException("The cellphone is invalid");
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = (value != string.Empty) ? value : throw new ArgumentException("The email is invalid");
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Person left, Person right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }

    }
}
