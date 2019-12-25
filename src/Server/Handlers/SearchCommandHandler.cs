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
            object data = ServerAnswer.INVALID_COMMAND;

            switch (DataPacket.Command.TypeData)
            {
                case TypeData.PERSON:
                    PersonService personService = new PersonService();
                    data = personService.Search(DataPacket.Data as string);
                    break;
                case TypeData.EMPLOYEE:
                    EmployeeService employeeService = new EmployeeService();
                    data = employeeService.Search(DataPacket.Data as string);
                    break;
                case TypeData.ADMINISTRATIVE_EMPLOYEE:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    data = administrativeEmployeeService.Search(DataPacket.Data as string);
                    break;
                case TypeData.BANKDRAFT:
                    BankDraftService bankDraftService = new BankDraftService();
                    data = bankDraftService.Search(DataPacket.Data as string);
                    break;
                case TypeData.COMMEND:
                    CommendService commendService = new CommendService();
                    data = commendService.Search(DataPacket.Data as string);
                    break;
                case TypeData.ROUTE:
                    RouteService routeService = new RouteService();
                    data = routeService.Search(DataPacket.Data as string);
                    break;
                case TypeData.TRANSPORT_FORM:
                    TransportFormService transportFormService = new TransportFormService();
                    data = transportFormService.Search(DataPacket.Data as string);
                    break;
                case TypeData.VEHICLE:
                    VehicleService vehicleService = new VehicleService();
                    data = vehicleService.Search(DataPacket.Data as string);
                    break;
                case TypeData.CURRENT_TRANSPORT_FORM:
                    transportFormService = new TransportFormService();
                    data = transportFormService.CurrentTransportFrom(DataPacket.Data as string);
                    break;
            }

            if (data is null)
                data = ServerAnswer.NOT_FOUND_DATA;

            return data;
        }
    }
}
