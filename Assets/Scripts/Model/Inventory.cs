using UnityEngine;

namespace JevLogin
{
    public class Inventory : IInitialization
    {
        private Weapon[] _weapons = new Weapon[5];

        public Weapon[] Weapons => _weapons;

        public FlashLightModel FlashLightModel { get; private set; }

        public void Initialization()
        {
            _weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().GetComponentsInChildren<Weapon>();

            foreach (var weapon in Weapons)
            {
                weapon.IsVisible = false;
            }

            FlashLightModel = Object.FindObjectOfType<FlashLightModel>();
            FlashLightModel.Switch(FlashLightActiveType.Off);
        }

        //todo добавить функционал
        public void RemoveWeapon(Weapon weapon)
        {

        }
    }
}