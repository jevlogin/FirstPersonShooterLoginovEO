namespace JevLogin
{
    public class WeaponController : BaseController
    {
        #region Fields

        private Weapon _weapon;

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] weapon)
        {
            if (IsActive) return;

            if (weapon.Length > 0) _weapon = weapon[0] as Weapon;

            if (weapon == null) return;

            base.On(_weapon);
            _weapon.IsVisible = true;
            UiInterface.WeaponUiText.SetActive(true);
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;
            UiInterface.WeaponUiText.SetActive(false);
        }

        public void Fire()
        {
            _weapon.Fire();
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }

        public void ReloadClip()
        {
            _weapon.ReloadClip();
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }

        #endregion
    }
}
