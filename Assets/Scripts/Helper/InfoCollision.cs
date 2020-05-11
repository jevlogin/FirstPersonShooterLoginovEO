using UnityEngine;


namespace JevLogin
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Vector3 _direction;
        private readonly float _damage;

        #endregion


        #region Properties

        public Vector3 Direction => _direction;
        public float Damage => _damage;

        #endregion


        #region ClassLifeCycle

        public InfoCollision(float damage, Vector3 direction = default)
        {
            _damage = damage;
            _direction = direction;
        }

        #endregion
    }
}