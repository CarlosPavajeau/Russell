﻿using BusinessLogicLayer;
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
                case ClientRequest.GET_ALL_BANKDRAFTS:
                    BankDraftService bankDraftService = new BankDraftService();
                    data = bankDraftService.GetAllData();
                    break;
                case ClientRequest.GET_ALL_COMMENDS:
                    CommendService commendService = new CommendService();
                    data = commendService.GetAllData();
                    break;
                case ClientRequest.GET_ALL_ROUTES:
                    RouteService routeService = new RouteService();
                    data = routeService.GetAllData();
                    break;
                case ClientRequest.GET_ALL_TRANSPORT_FORMS:
                    TransportFormService transportFormService = new TransportFormService();
                    data = transportFormService.GetAllData();
                    break;
                case ClientRequest.GET_ALL_VEHICLES:
                    VehicleService vehicleService = new VehicleService();
                    data = vehicleService.GetAllData();
                    break;
                case ClientRequest.GET_ALL_EMPLOYEES:
                    EmployeeService employeeService = new EmployeeService();
                    data = employeeService.GetAllData();
                    break;
                case ClientRequest.GET_ALL_PEOPLE:
                    PersonService personService = new PersonService();
                    data = personService.GetAllData();
                    break;
                case ClientRequest.IS_IT_THE_FIRST_APPLICATION_START:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    if (administrativeEmployeeService.IsEmpty())
                        data = ServerAnswer.IS_THE_FIRST_APPLICATION_START;
                    else
                        data = ServerAnswer.IS_NOT_THE_FIRST_APPLICATION_START;
                    break;
                case ClientRequest.GET_DELIVERIES_COUNT:
                    commendService = new CommendService();
                    data = commendService.Count;
                    break;
                case ClientRequest.GET_TRANSPORT_FORM_COUNT:
                    transportFormService = new TransportFormService();
                    data = transportFormService.Count;
                    break;
                case ClientRequest.GET_ALL_DESTINATIONS:
                    routeService = new RouteService();
                    data = routeService.Destinations;
                    break;
            }

            return data;
        }
    }
}
