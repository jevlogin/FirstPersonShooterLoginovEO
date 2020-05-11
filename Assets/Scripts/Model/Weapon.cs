using System.Collections.Generic;
using UnityEngine;


namespace JevLogin
{
    public abstract class Weapon : BaseObjectScene
    {
        #region Fields

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999.0f;
        [SerializeField] protected float _rechargeTime = 0.2f;

        private Queue<Clip> _clips = new Queue<Clip>();

        private int _maxCountAmmunition = 40;
        private int _minCountAmmunition = 20;
        private int _countClip = 5;

        protected ITimeRemaining _timeRemaining;

        protected bool _isReady = true;

        public Ammunition Ammunition;
        public Clip Clip;
        public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };

        #endregion


        #region Properties

        public int CountClip => _clips.Count;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);

            for (var i = 0; i < _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = UnityEngine.Random.Range(_minCountAmmunition, _maxCountAmmunition) });
            }
            ReloadClip();
        }

        #endregion


        #region Methods

        public void ReloadClip()
        {
            if (CountClip <= 0) return;
            Clip = _clips.Dequeue();
        }

        private void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        public abstract void Fire();

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        #endregion
    }
}
