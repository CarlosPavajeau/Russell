using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public enum AccessLevel
    {
        LOW,
        MEDIUM,
        HIGH
    }
    public class User
    {
        public User(AccessData accessData, AccessLevel accessLevel)
        {
            AccessData = accessData;
            AccessLevel = accessLevel;
        }

        public AccessData AccessData
        {
            get;
            set;
        }

        public AccessLevel AccessLevel
        {
            get;
            set;
        }

        public bool IsLowUser()
        {
            return AccessLevel == AccessLevel.LOW;
        }

        public bool IsMediumUser()
        {
            return AccessLevel == AccessLevel.MEDIUM;
        }

        public bool IsHighUser()
        {
            return AccessLevel == AccessLevel.HIGH;
        }
    }
}
