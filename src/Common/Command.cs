namespace Common
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

    public enum TypeCommand
    {
        SAVE,
        SEARCH,
        UPDATE,
        DELETE
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
