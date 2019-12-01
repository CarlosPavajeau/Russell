namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.StartListening();
        }
    }
}
