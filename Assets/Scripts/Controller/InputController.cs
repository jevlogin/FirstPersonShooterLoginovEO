using System;
using UnityEngine;


namespace JevLogin
{
    public sealed class InputController : BaseController, IExecute
    {
        #region PrivateData

        private KeyCode _activateFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _removeWeapon = KeyCode.G;
        private int _mouseButton = (int)MouseButton.LeftButton;

        #endregion


        #region ClassLifeCycle

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion


        #region Methods

        public void Execute()
        {
            if (!IsActive) return;

            if (Input.GetKeyDown(_activateFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLightModel);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectWeapon(0);
            }
            else if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }
            else if (Input.GetKeyDown(_reloadClip))
            {
                ServiceLocator.Resolve<WeaponController>().ReloadClip();
            }
            else if (Input.GetKeyDown(_removeWeapon))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<Inventory>().RemoveWeapon();
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                MouseScroll(MouseScrollWheel.Up);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                MouseScroll(MouseScrollWheel.Down);
            }
        }

        private void MouseScroll(MouseScrollWheel value)
        {
            var tempWeapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(value);
            SelectWeapon(tempWeapon);
        }

        /// <summary>
        /// Выбор оружия по индексу
        /// </summary>
        /// <param name="indexWeapon">Индекс оружия</param>
        private void SelectWeapon(int indexWeapon)
        {
            var tempWeapon = ServiceLocator.Resolve<Inventory>().SelectWeapon(indexWeapon);
            SelectWeapon(tempWeapon);
        }

        /// <summary>
        /// Выбор оружия по оружию
        /// </summary>
        /// <param name="weapon">Само оружие</param>
        private void SelectWeapon(Weapon weapon)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            if (weapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(weapon);
            }
        }

        #endregion
    }
}
