namespace JevLogin
{
    public sealed class Gun : Weapon
    {
        public override void Fire()
        {
            if (!_isReady) return;

            if (Clip.CountAmmunition <= 0) return;

            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);   //todo надо будет сделать Pool объектов
            temAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}