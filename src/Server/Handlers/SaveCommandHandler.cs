using BusinessLogicLayer;
using Common;
using Entity;

namespace Server.Handlers
{
    public sealed class SaveCommandHandler : CommandHandler
    {
        public SaveCommandHandler(DataPacket dataPacket) : base(dataPacket)
        {

        }

        public override object HandleCommand()
        {
            ServerAnswer answer = ServerAnswer.INVALID_COMMAND;

            switch (DataPacket.Command.TypeData)
            {
                case TypeData.PERSON:
                    PersonService personService = new PersonService();
                    answer = (personService.Save(DataPacket.Data as Person)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.EMPLOYEE:
                    EmployeeService employeeService = new EmployeeService();
                    answer = (employeeService.Save(DataPacket.Data as Employee)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.ADMINISTRATIVE_EMPLOYEE:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    answer = (administrativeEmployeeService.Save(DataPacket.Data as AdministrativeEmployee)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.BANKDRAFT:
                    BankDraftService bankDraftService = new BankDraftService();
                    answer = (bankDraftService.Save(DataPacket.Data as BankDraft)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.COMMEND:
                    CommendService commendService = new CommendService();
                    answer = (commendService.Save(DataPacket.Data as Commend)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.ROUTE:
                    RouteService routeService = new RouteService();
                    answer = (routeService.Save(DataPacket.Data as Route)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.VEHICLE:
                    VehicleService vehicleService = new VehicleService();
                    answer = (vehicleService.Save(DataPacket.Data as Vehicle)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
                case TypeData.TRANSPORT_FORM:
                case TypeData.TICKET:
                    TransportFormService transportFormService = new TransportFormService();

                    if (DataPacket.Command.TypeData == TypeData.TRANSPORT_FORM)
                        answer = (transportFormService.Save(DataPacket.Data as TransportForm)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    else
                        answer = (transportFormService.Save(DataPacket.Data as Ticket)) ? ServerAnswer.SAVE_SUCCESSFUL : ServerAnswer.DATA_ALREADY_REGISTERED;
                    break;
            }

            return answer;
        }
    }
}
