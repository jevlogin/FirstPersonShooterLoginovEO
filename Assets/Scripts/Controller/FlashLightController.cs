using UnityEngine;


namespace JevLogin
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private FlashLightModel _flashLightModel;

        #endregion


        #region Methods

        public void Initialization()
        {
            UiInterface.FlashLightUiText.SetActive(false);
            UiInterface.FlashLightUiBar.SetActive(false);
        }

        public override void On(params BaseObjectScene[] flashLight)
        {
            if (IsActive) return;

            if (flashLight.Length > 0)
            {
                _flashLightModel = flashLight[0] as FlashLightModel;    //todo Нужно пояснение к этой строке...
            }

            if (_flashLightModel == null) return;

            if (_flashLightModel.BatteryChargeCurrent <= 0) return;

            base.On(_flashLightModel);

            _flashLightModel.Switch(FlashLightActiveType.On);

            UiInterface.FlashLightUiText.SetActive(true);
            UiInterface.FlashLightUiBar.SetActive(true);
            UiInterface.FlashLightUiBar.SetColor(Color.green);

        }
        public void Execute()
        {
            if (!IsActive) return;

            if (_flashLightModel.EditBatteryCharge())
            {
                UiInterface.FlashLightUiText.Text = _flashLightModel.BatteryChargeCurrent;
                UiInterface.FlashLightUiBar.Fill = _flashLightModel.Charge;

                _flashLightModel.Rotation();

                if (_flashLightModel.LowBattery())
                {
                    UiInterface.FlashLightUiBar.SetColor(Color.red);
                }
            }
            else
            {
                Off();
            }
        }

        public override void Off()
        {
            if (!IsActive) return;

            base.Off();

            _flashLightModel.Switch(FlashLightActiveType.Off);
            UiInterface.FlashLightUiBar.SetActive(false);
            UiInterface.FlashLightUiText.SetActive(false);
        }

        #endregion
    }
}
