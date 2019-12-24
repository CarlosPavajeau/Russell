using Common;
using BusinessLogicLayer;

namespace Server.Handlers
{
    public class UpdateCommandHandler : CommandHandler
    {
        public UpdateCommandHandler(DataPacket dataPacket) : base(dataPacket)
        {

        }

        public override object HandleCommand()
        {
            bool result = false;

            switch (DataPacket.Command.TypeData)
            {
                case TypeData.EMPLOYEE:
                    break;
                case TypeData.ADMINISTRATIVE_EMPLOYEE:
                    break;
                case TypeData.BANKDRAFT:
                    break;
                case TypeData.COMMEND:
                    break;
                case TypeData.ROUTE:
                    break;
                case TypeData.TRANSPORT_FORM:
                    break;
                case TypeData.VEHICLE:
                    break;
                case TypeData.TICKET:
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
