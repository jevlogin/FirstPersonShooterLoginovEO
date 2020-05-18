using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace JevLogin
{
    public class Inventory : IInitialization
    {
        #region Fields

        private List<Weapon> _weapons = new List<Weapon>();

        private int _selectIndexWeapon;

        #endregion


        #region Properties

        public FlashLightModel FlashLightModel { get; private set; }
        public List<Weapon> Weapons => _weapons;

        #endregion


        #region Methods

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
                GetComponentsInChildren<Weapon>().ToList();

            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }

            FlashLightModel = Object.FindObjectOfType<FlashLightModel>();
            FlashLightModel.Switch(FlashLightActiveType.Off);
        }

        /// <summary>
        /// Возвращаем оружие по индексу
        /// </summary>
        /// <param name="weaponIndex">Индекс оружия из списка оружий</param>
        /// <returns></returns>
        public Weapon SelectWeapon(int weaponIndex)
        {
            if (weaponIndex < 0 || weaponIndex >= Weapons.Count)
            {
                return null;
            }
            var tempWeapon = Weapons[weaponIndex];
            return tempWeapon;
        }

        public Weapon SelectWeapon(MouseScrollWheel scrollWheel)
        {
            if (scrollWheel == MouseScrollWheel.Up)
            {
                if (_selectIndexWeapon < Weapons.Count - 1)
                {
                    _selectIndexWeapon++;
                }
                else
                {
                    _selectIndexWeapon--;
                }
                return SelectWeapon(_selectIndexWeapon);
            }
            if (_selectIndexWeapon <= 0)
            {
                _selectIndexWeapon = Weapons.Count;
            }
            else
            {
                _selectIndexWeapon--;
            }
            return SelectWeapon(_selectIndexWeapon);
        }

        public void RemoveWeapon()
        {
            var selectWeapon = SelectWeapon(_selectIndexWeapon);
            if (selectWeapon)
            {
                Weapons.Remove(selectWeapon);
                selectWeapon.transform.parent = null;
                selectWeapon.SetActive(true);
            }
        }

        #endregion
    }
}