using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface ISave<T>
    {
        bool Save(T data);
    }
}
