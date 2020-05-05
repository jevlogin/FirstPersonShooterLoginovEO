using UnityEngine;


namespace JevLogin
{
    public sealed class Controllers : IInitialization
    {
        #region Fields

        private readonly IExecute[] _executeControllers;

        #endregion


        #region Properties

        public IExecute this[int index] => _executeControllers[index];
        public int Length => _executeControllers.Length;

        #endregion


        #region ClassLifeCycles

        public Controllers()
        {
            IMotor motor = default;
            if (Application.platform == RuntimePlatform.PS4)
            {
                //todo SonyPlaystation
            }
            else
            {
                motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            }
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            _executeControllers = new IExecute[3];

            _executeControllers[0] = ServiceLocator.Resolve<PlayerController>();
            _executeControllers[1] = ServiceLocator.Resolve<FlashLightController>();
            _executeControllers[2] = ServiceLocator.Resolve<InputController>();
        }

        #endregion


        #region Methods

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<InputController>().On();
        }

        #endregion
    }
}
