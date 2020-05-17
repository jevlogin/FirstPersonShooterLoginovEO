using UnityEngine;


namespace JevLogin
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Transform _objectCollision;
        private readonly ContactPoint _contact;
        private readonly Vector3 _direction;

        private readonly float _damage;

        #endregion


        #region Properties

        public Vector3 Direction => _direction;
        public Transform ObjectCollision => _objectCollision;
        public ContactPoint Contact => _contact;
        public float Damage => _damage;

        #endregion


        #region ClassLifeCycle

        public InfoCollision(float damage, ContactPoint contact, Transform objectCollision, Vector3 direction = default)
        {
            _damage = damage;
            _direction = direction;
            _contact = contact;
            _objectCollision = objectCollision;
        }

        #endregion
    }
}