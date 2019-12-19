using BusinessLogicLayer;
using Common;

namespace Server
{
    public static class ClientRequestHanlder
    {
        public static object ProccessClientRequest(ClientRequest request)
        {
            switch (request)
            {
                case ClientRequest.IS_IT_THE_FIRST_APPLICATION_START:
                    AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();
                    if (!administrativeEmployeeService.IsEmpty())
                        return ServerAnswer.IS_THE_FIRST_APPLICATION_START;
                    else
                        return ServerAnswer.IS_NOT_THE_FIRST_APPLICATION_START;
                default:
                    return null;
            }
        }
    }
}
