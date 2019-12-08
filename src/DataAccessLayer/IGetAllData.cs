using System.Collections.Generic;

namespace DataAccessLayer
{
    public interface IGetAllData<T>
    {
        IList<T> GetAllData();
    }
}
