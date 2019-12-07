namespace Entity.Common
{
    [System.Serializable]
    public class SaveCommand : Command
    {
        public SaveCommand(TypeData typeData) : base(typeData)
        {

        }
    }
}
