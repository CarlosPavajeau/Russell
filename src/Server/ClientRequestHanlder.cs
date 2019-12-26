using BusinessLogicLayer;
using Common;

namespace Server
{
    public static class ClientRequestHanlder
    {
        public static object ProccessClientRequest(ClientRequest request)
        {
            object data = null;

            switch (request)
            {
                case ClientRequest.GetBankDrafts:
                    BankDraftService bankDraftService = new BankDraftService();
                    data = bankDraftService.GetAllData();
                    break;
                case ClientRequest.GetCommends:
                    CommendService commendService = new CommendService();
                    data = commendService.GetAllData();
                    break;
                case ClientRequest.GetRoutes:
                    RouteService routeService = new RouteService();
                    data = routeService.GetAllData();
                    break;
                case ClientRequest.GetTransportsForms:
                    TransportFormService transportFormService = new TransportFormService();
                    data = transportFormService.GetAllData();
                    break;
                case ClientRequest.GetVehicles:
                    VehicleService vehicleService = new VehicleService();
                    data = vehicleService.GetAllData();
                    break;
                case ClientRequest.GetEmployees:
                    EmployeeService employeeService = new EmployeeService();
                    data = employeeService.GetAllData();
                    break;
                case ClientRequest.GetPeople:
                    PersonService personService = new PersonService();
                    data = personService.GetAllData();
                    break;
                case ClientRequest.IsTheFirstApplicationStart:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    if (administrativeEmployeeService.IsEmpty())
                        data = ServerAnswer.IsTheFirstApplicationStart;
                    else
                        data = ServerAnswer.IsNotTheFirstApplicationStart;
                    break;
                case ClientRequest.GetDeliveriesCount:
                    commendService = new CommendService();
                    data = commendService.Count;
                    break;
                case ClientRequest.GetTransportFormCount:
                    transportFormService = new TransportFormService();
                    data = transportFormService.Count;
                    break;
                case ClientRequest.GetDestinations:
                    routeService = new RouteService();
                    data = routeService.Destinations;
                    break;
            }

            return data;
        }
    }
}
