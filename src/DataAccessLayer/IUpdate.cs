namespace DataAccessLayer
{
    public interface IUpdate
    {
        bool Update(string primarykey, string columToModify, object newValue);
    }
}
