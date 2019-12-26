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
            ServerAnswer answer = ServerAnswer.InvalidCommand;

            switch (DataPacket.Command.TypeData)
            {
                case TypeData.Person:
                    PersonService personService = new PersonService();
                    answer = (personService.Save(DataPacket.Data as Person)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.Employee:
                    EmployeeService employeeService = new EmployeeService();
                    answer = (employeeService.Save(DataPacket.Data as Employee)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.AdministrativeEmployee:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    answer = (administrativeEmployeeService.Save(DataPacket.Data as AdministrativeEmployee)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.BankDraft:
                    BankDraftService bankDraftService = new BankDraftService();
                    answer = (bankDraftService.Save(DataPacket.Data as BankDraft)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.Commend:
                    CommendService commendService = new CommendService();
                    answer = (commendService.Save(DataPacket.Data as Commend)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.Route:
                    RouteService routeService = new RouteService();
                    answer = (routeService.Save(DataPacket.Data as Route)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.Vehicle:
                    VehicleService vehicleService = new VehicleService();
                    answer = (vehicleService.Save(DataPacket.Data as Vehicle)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
                case TypeData.TransportForm:
                case TypeData.Ticket:
                    TransportFormService transportFormService = new TransportFormService();

                    if (DataPacket.Command.TypeData == TypeData.TransportForm)
                        answer = (transportFormService.Save(DataPacket.Data as TransportForm)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    else
                        answer = (transportFormService.Save(DataPacket.Data as Ticket)) ? ServerAnswer.SaveSuccessful : ServerAnswer.DataAlreadyRegistered;
                    break;
            }

            return answer;
        }
    }
}
