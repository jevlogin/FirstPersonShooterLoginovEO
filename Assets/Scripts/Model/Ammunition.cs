using System;
using UnityEngine;


namespace JevLogin
{
    public abstract class Ammunition : BaseObjectScene
    {
        [SerializeField] private float _timeToDestruct = 10.0f;
        [SerializeField] private float _baseDamage = 10.0f;
        protected float _curDamage; //todo сделать свой
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _timeRemaining;

        public AmmunitionType Type = AmmunitionType.Bullet;

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        private void Start()
        {
            Destroy(gameObject, _timeToDestruct);
            _timeRemaining = new TimeRemaining(LossOfDamage, 1.0f, true);
            _timeRemaining.AddTimeRemaining();
        }

        public void AddForce(Vector3 direction)
        {
            if (!Rigidbody) return;

            Rigidbody.AddForce(direction);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition()
        {
            Destroy(gameObject);
            _timeRemaining.RemoveTimeRemaining();
            //todo вернуть в pool
        }

    }
}