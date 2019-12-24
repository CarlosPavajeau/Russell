using Common;
using Server.Handlers;

namespace Server
{
    public static class DataPacketHandler
    {
        public static object HandleDataPacket(DataPacket dataPacket)
        {
            CommandHandler commandHandler = null;

            if (dataPacket.Command is SearchCommand)
                commandHandler = new SearchCommandHandler(dataPacket);

            if (dataPacket.Command is SaveCommand)
                commandHandler = new SaveCommandHandler(dataPacket);

            if (dataPacket.Command is DeleteCommand)
                commandHandler = new DeleteCommandHandler(dataPacket);

            if (dataPacket.Command is UpdateCommand)
                commandHandler = new UpdateCommandHandler(dataPacket);

            return commandHandler?.HandleCommand();
        }
    }
}
