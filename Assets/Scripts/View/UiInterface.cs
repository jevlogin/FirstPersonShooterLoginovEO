using UnityEngine;


namespace JevLogin
{
    public class UiInterface
    {
        #region Fields

        private FlashLightUiText _flashLightUiText;
        private FlashLightUiBar _flashLightUiBar;
        private SelectionObjectMessageUi _selectionObjectMessageUi;
        private WeaponUiText _weaponUiText;

        #endregion


        #region Properties

        public FlashLightUiText FlashLightUiText
        {
            get
            {
                if (!_flashLightUiText)
                {
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                }
                return _flashLightUiText;
            }
        }

        public FlashLightUiBar FlashLightUiBar
        {
            get
            {
                if (!_flashLightUiBar)
                {
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                }
                return _flashLightUiBar;
            }
        }

        public SelectionObjectMessageUi SelectionObjectMessageUi
        {
            get
            {
                if (!_selectionObjectMessageUi)
                {
                    _selectionObjectMessageUi = Object.FindObjectOfType<SelectionObjectMessageUi>();
                }
                return _selectionObjectMessageUi;
            }
        }

        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                {
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                }
                return _weaponUiText;
            }
        }

        #endregion
    }
}
