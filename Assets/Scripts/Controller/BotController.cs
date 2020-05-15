using System.Collections.Generic;

namespace JevLogin
{
    public class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private readonly int _countBot = 0;
        private readonly HashSet<Bot> _botList = new HashSet<Bot>();

        #endregion


        #region IExecute

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
