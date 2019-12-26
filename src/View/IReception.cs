namespace View
{
    public delegate void Recieve<T>(T data);
    public interface IReception<T>
    {
        void Receive(T data);
    }
}
