namespace Entity.Common
{
    public enum TypeData
    {
        PERSON, 
        EMPLOYEE,
        ADMINISTRATIVE_EMPLOYEE,
        BANKDRAFT,
        COMMEND,
        ROUTE,
        TRANSPORT_FORM,
        VEHICLE,
        TICKET
    }

    [System.Serializable]
    public abstract class Command
    {
        public Command(TypeData typeData)
        {
            TypeData = typeData;
        }

        public TypeData TypeData { get; }
    }
}
