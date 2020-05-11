namespace JevLogin
{
    public sealed class Wall : BaseObjectScene, ISelectObject
    {
        public string GetMessage()
        {
            return Name;
        }
    }
}
