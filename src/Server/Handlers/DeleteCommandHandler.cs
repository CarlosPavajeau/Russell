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
                case TypeData.Employee:
                    EmployeeService employeeService = new EmployeeService();
                    result = employeeService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.AdministrativeEmployee:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    result = administrativeEmployeeService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.Route:
                    RouteService routeService = new RouteService();
                    result = routeService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.Vehicle:
                    VehicleService vehicleService = new VehicleService();
                    result = vehicleService.Delete(DataPacket.Data as string);
                    break;
                case TypeData.Ticket:
                    TransportFormService transportFormService = new TransportFormService();
                    result = transportFormService.DeleteTicket(DataPacket.Data as string);
                    break;
            }

            return result ? ServerAnswer.SuccessfullyRemoved : ServerAnswer.DeletionFailed;
        }
    }
}
