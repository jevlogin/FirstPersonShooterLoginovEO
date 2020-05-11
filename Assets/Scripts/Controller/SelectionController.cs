using System;
using UnityEngine;


namespace JevLogin
{
    public class SelectionController : BaseController, IExecute
    {
        #region Fields

        private readonly Camera _mainCamera;
        private GameObject _dedicatedObject;
        private ISelectObject _selectedObject;
        private readonly Vector2 _center;

        private readonly float _dedicateDistance = 20.0f;
        private bool _nullString;
        private bool _isSelectedObject;

        #endregion


        #region ClassLifeCycles

        public SelectionController()
        {
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }

        #endregion

        public void Execute()
        {
            if (!IsActive) return;

            if (Physics.Raycast(_mainCamera.ScreenPointToRay(_center), out var hit, _dedicateDistance))
            {
                SelectObject(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                UiInterface.SelectionObjectMessageUi.Text = String.Empty;
                _nullString = true;
                _dedicatedObject = null;
                _isSelectedObject = false;
            }

            if (_isSelectedObject)
            {
                //todo Действие над объектом

                switch (_selectedObject)
                {
                    case Weapon aim:
                        //todo в инвентарь
                        //todo Inventory.AddWeapon(aim);
                        break;
                    case Wall wall:
                        break;
                    default:
                        break;
                }
            }
        }

        private void SelectObject(GameObject gameObject)
        {
            if (gameObject == _dedicatedObject) return;

            _selectedObject = gameObject.GetComponent<ISelectObject>();

            if (_selectedObject != null)
            {
                UiInterface.SelectionObjectMessageUi.Text = _selectedObject.GetMessage();
                //todo реализовать показ здоровья противника
                _isSelectedObject = true;
            }
            else
            {
                UiInterface.SelectionObjectMessageUi.Text = String.Empty;
                _isSelectedObject = false;
            }

            _dedicatedObject = gameObject;
        }
    }
}
