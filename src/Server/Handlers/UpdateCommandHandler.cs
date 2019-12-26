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
                case TypeData.Employee:
                    break;
                case TypeData.AdministrativeEmployee:
                    break;
                case TypeData.BankDraft:
                    break;
                case TypeData.Commend:
                    break;
                case TypeData.Route:
                    break;
                case TypeData.TransportForm:
                    break;
                case TypeData.Vehicle:
                    break;
                case TypeData.Ticket:
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
