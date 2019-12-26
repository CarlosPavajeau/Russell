namespace Common
{
    public enum TypeData
    {
        Person, 
        Employee,
        AdministrativeEmployee,
        BankDraft,
        Commend,
        Route,
        TransportForm,
        CurrentTransportForm,
        Vehicle,
        Ticket
    }

    public enum TypeCommand
    {
        Save,
        Search,
        Update,
        Delete
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
