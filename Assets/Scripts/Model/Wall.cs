namespace JevLogin
{
    public sealed class Wall : Environment, ISelectObject
    {
        #region Methods

        public string GetMessage()
        {
            return Name;
        }

        #endregion
    }
}
