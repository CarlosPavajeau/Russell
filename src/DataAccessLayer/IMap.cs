using System.Data.Common;

namespace DataAccessLayer
{
    public interface IMap<T>
    {
        T Map(DbDataReader dbDataReader);
    }
}
