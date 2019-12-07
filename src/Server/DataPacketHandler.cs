using BusinessLogicLayer;
using Entity.Common;
using Entity;

namespace Server
{
    public static class DataPacketHandler
    {
        public static object HandleDataPacket(DataPacket dataPacket)
        {
            if (dataPacket.Command is SearchCommand searchCommand)
            {
                object data = HandleSearchCommand(searchCommand, dataPacket.Data as string);

                if (data is null)
                    return ServerAnswer.NOT_FOUND_DATA;
                else return data;
            }
            else if (dataPacket.Command is SaveCommand)
            {
                return HandleSaveCommand(dataPacket);
            }
            else
                return ServerAnswer.NOT_FOUND_DATA;
        }

        private static ServerAnswer HandleSaveCommand(DataPacket dataPacket)
        {
            switch (dataPacket.Command.TypeData)
            {
                case TypeData.PERSON:
                    PersonService personService = new PersonService();
                    if (!personService.Save(dataPacket.Data as Person))
                        return ServerAnswer.DATA_ALREADY_REGISTERED;
                    return ServerAnswer.SAVE_SUCCESSFUL;
                case TypeData.EMPLOYEE:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.ADMINISTRATIVE_EMPLOYEE:
                    if (!new AdministrativeEmployeeService().Save(dataPacket.Data as AdministrativeEmployee))
                        return ServerAnswer.DATA_ALREADY_REGISTERED;
                    return ServerAnswer.SAVE_SUCCESSFUL;
                case TypeData.BANKDRAFT:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.COMMEND:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.ROUTE:
                    if (!new RouteService().Save(dataPacket.Data as Route))
                        return ServerAnswer.DATA_ALREADY_REGISTERED;
                    return ServerAnswer.SAVE_SUCCESSFUL;
                case TypeData.TRANSPORT_FORM:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.VEHICLE:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.TICKET:
                    return ServerAnswer.NOT_FOUND_DATA;
                default:
                    return ServerAnswer.NOT_FOUND_DATA;
            }
        }

        private static object HandleSearchCommand(SearchCommand searchCommand, string primaryKey)
        {
            switch (searchCommand.TypeData)
            {
                case TypeData.PERSON:
                    PersonService personService = new PersonService();
                    return personService.Search(primaryKey);
                case TypeData.EMPLOYEE:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.ADMINISTRATIVE_EMPLOYEE:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    return administrativeEmployeeService.Search(primaryKey);
                case TypeData.BANKDRAFT:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.COMMEND:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.ROUTE:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.TRANSPORT_FORM:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.VEHICLE:
                    return ServerAnswer.NOT_FOUND_DATA;
                case TypeData.TICKET:
                    return ServerAnswer.NOT_FOUND_DATA;
                default:
                    return ServerAnswer.NOT_FOUND_DATA;
            }
        }
    }
}
