using UnityEngine;


namespace JevLogin
{
    public class Inventory : IInitialization
    {
        #region Fields

        private Weapon[] _weapons = new Weapon[5];

        #endregion


        #region Properties

        public Weapon[] Weapons => _weapons;
        public FlashLightModel FlashLightModel { get; private set; }

        #endregion


        #region Methods

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

        #endregion
    }
}