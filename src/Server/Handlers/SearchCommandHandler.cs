using BusinessLogicLayer;
using Common;

namespace Server.Handlers
{
    public class SearchCommandHandler : CommandHandler
    {
        public SearchCommandHandler(DataPacket dataPacket) : base(dataPacket)
        {

        }

        public override object HandleCommand()
        {
            object data = ServerAnswer.InvalidCommand;

            switch (DataPacket.Command.TypeData)
            {
                case TypeData.Person:
                    PersonService personService = new PersonService();
                    data = personService.Search(DataPacket.Data as string);
                    break;
                case TypeData.Employee:
                    EmployeeService employeeService = new EmployeeService();
                    data = employeeService.Search(DataPacket.Data as string);
                    break;
                case TypeData.AdministrativeEmployee:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    data = administrativeEmployeeService.Search(DataPacket.Data as string);
                    break;
                case TypeData.BankDraft:
                    BankDraftService bankDraftService = new BankDraftService();
                    data = bankDraftService.Search(DataPacket.Data as string);
                    break;
                case TypeData.Commend:
                    CommendService commendService = new CommendService();
                    data = commendService.Search(DataPacket.Data as string);
                    break;
                case TypeData.Route:
                    RouteService routeService = new RouteService();
                    data = routeService.Search(DataPacket.Data as string);
                    break;
                case TypeData.TransportForm:
                    TransportFormService transportFormService = new TransportFormService();
                    data = transportFormService.Search(DataPacket.Data as string);
                    break;
                case TypeData.Vehicle:
                    VehicleService vehicleService = new VehicleService();
                    data = vehicleService.Search(DataPacket.Data as string);
                    break;
                case TypeData.CurrentTransportForm:
                    transportFormService = new TransportFormService();
                    data = transportFormService.CurrentTransportFrom(DataPacket.Data as string);
                    break;
            }

            if (data is null)
                data = ServerAnswer.NotFoundData;

            return data;
        }
    }
}
