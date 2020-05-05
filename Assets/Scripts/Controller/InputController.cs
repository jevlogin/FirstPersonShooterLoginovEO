using UnityEngine;


namespace JevLogin
{
    public sealed class InputController : BaseController, IExecute
    {
        #region PrivateData

        private KeyCode _activateFlashLight = KeyCode.F;

        #endregion


        #region Methods

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }
            if (Input.GetKeyDown(_activateFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }
        }

        #endregion
    }
}
