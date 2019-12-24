using Common;

namespace Server.Handlers
{
    public abstract class CommandHandler
    {
        protected DataPacket DataPacket { get; }
        public CommandHandler(DataPacket dataPacket)
        {
            DataPacket = dataPacket;
        }

        public abstract object HandleCommand();
    }
}
