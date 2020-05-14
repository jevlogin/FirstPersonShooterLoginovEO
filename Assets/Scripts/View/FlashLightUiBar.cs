using UnityEngine;
using UnityEngine.UI;


namespace JevLogin
{
    public sealed class FlashLightUiBar : MonoBehaviour
    {
        #region Fields

        private Image _imageUiBar;

        #endregion


        #region Properties

        public float Fill
        {
            set => _imageUiBar.fillAmount = value;
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _imageUiBar = GetComponent<Image>();
        }

        #endregion


        #region Methods

        public void SetActive(bool value)
        {
            _imageUiBar.gameObject.SetActive(value);
        }

        public void SetColor(Color color)
        {
            _imageUiBar.color = color;
        }

        #endregion
    }
}
