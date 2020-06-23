using Common;
using BusinessLogicLayer;
using Entity;
using System.Collections.Generic;

namespace Server.Handlers
{
    public class UpdateCommandHandler : CommandHandler
    {
        public UpdateCommandHandler(DataPacket dataPacket) : base(dataPacket)
        {

        }

        public override object HandleCommand()
        {
            ServerAnswer result = ServerAnswer.FailedModification;
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
                    KeyValuePair<string, Dictionary<FinalcialInformationType, decimal>> valuesToUpdate = 
                        (KeyValuePair<string, Dictionary<FinalcialInformationType, decimal>>) DataPacket.Data;
                    TransportFormService transportFormService = new TransportFormService();
                    if (transportFormService.UpdateFinancialInformation(valuesToUpdate.Key, valuesToUpdate.Value))
                        result = ServerAnswer.SuccessfullyModified;
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
