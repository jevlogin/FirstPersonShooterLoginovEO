namespace JevLogin
{
    public sealed class Wall : BaseObjectScene, ISelectObject
    {
        #region Methods

        public string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}
