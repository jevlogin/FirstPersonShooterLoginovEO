using UnityEngine;


namespace JevLogin
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        private FlashLightUi _flashLightUi;

        #endregion


        #region Properties

        public bool IsActive { get; private set; }

        #endregion


        #region Methods

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUi = Object.FindObjectOfType<FlashLightUi>();
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            _flashLightModel.Rotation();

            if (_flashLightModel.EditBatteryCharge())
            {
                _flashLightUi.Text = _flashLightModel.BatteryChargeCurrent;
            }
            else
            {
                Off();
            }
        }

        public override void On()
        {
            if (IsActive)
            {
                return;
            }

            if (_flashLightModel.BatteryChargeCurrent <= 0)
            {
                return;
            }

            base.On();

            _flashLightModel.Switch(FlashLightActiveType.On);
            _flashLightUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive)
            {
                return;
            }

            base.Off();

            _flashLightModel.Switch(FlashLightActiveType.Off);
            _flashLightUi.SetActive(false);
        }

        #endregion
    }
}
