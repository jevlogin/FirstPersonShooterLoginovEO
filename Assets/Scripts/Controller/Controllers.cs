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
            IMotor motor =  new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());

            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectionController());

            _executeControllers = new IExecute[5];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();
            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();
            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();
            _executeControllers[3] = ServiceLocator.Resolve<InputController>();
            _executeControllers[4] = ServiceLocator.Resolve<SelectionController>();
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

            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<SelectionController>().On();
            ServiceLocator.Resolve<InputController>().On();
        }

        #endregion
    }
}
