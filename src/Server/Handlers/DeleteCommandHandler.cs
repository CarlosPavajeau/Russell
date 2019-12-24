using Common;
using BusinessLogicLayer;

namespace Server.Handlers
{
    public class DeleteCommandHandler : CommandHandler
    {
        public DeleteCommandHandler(DataPacket dataPacket) : base(dataPacket)
        {

        }

        public override object HandleCommand()
        {
            bool result = false;

            switch (DataPacket.Command.TypeData)
            {
                case TypeData.EMPLOYEE:
                    EmployeeService employeeService = new EmployeeService();
                    result = employeeService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.ADMINISTRATIVE_EMPLOYEE:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    result = administrativeEmployeeService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.ROUTE:
                    RouteService routeService = new RouteService();
                    result = routeService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.VEHICLE:
                    VehicleService vehicleService = new VehicleService();
                    result = vehicleService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.TICKET:
                    //TransportFormService transportFormService = new TransportFormService();
                    result = false;
                    break;
            }

            return result;
        }
    }
}
