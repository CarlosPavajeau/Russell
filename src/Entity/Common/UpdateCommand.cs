namespace Entity.Common
{
    [System.Serializable]
    public class UpdateCommand : Command
    {
        public UpdateCommand(TypeData typeData) : base(typeData)
        {

        }
    }
}
