namespace DataAccessLayer
{
    public interface ISearch<T>
    {
        T Search(string primaryKey);
    }
}
