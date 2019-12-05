using BusinessLogicLayer;
using Entity.Common;
using Entity;

namespace Server
{
    public static class DataPacketHandler
    {
        public static object HandleDataPacket(DataPacket dataPacket)
        {
            if (dataPacket.Command == Command.SEARCH_ADMINISTRATIVE_EMPLOYEE)
            {
                AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                AdministrativeEmployee administrativeEmployee = administrativeEmployeeService.Search(dataPacket.Data as string);

                if (administrativeEmployee is null)
                    return ServerAnswer.NOT_FOUND_DATA;
                else
                    return administrativeEmployee;
            }
            else
                return ServerAnswer.NOT_FOUND_DATA;
        }
    }
}
