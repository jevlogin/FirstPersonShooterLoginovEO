﻿using System;
using UnityEngine;


namespace JevLogin
{
    public class Aim : MonoBehaviour, ICollision, ISelectObject
    {
        public event Action OnPointChange = delegate { };

        public float HealthPoint = 100.0f;
        private bool _isDead;
        private float _timeToDestroy = 10.0f;
        //todo дописать поглащение урона

        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (HealthPoint > 0)
            {
                HealthPoint -= info.Damage;
            }

            if (HealthPoint <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                Destroy(gameObject, _timeToDestroy);

                OnPointChange.Invoke();
                _isDead = true;
            }
        }

        public string GetMessage()
        {
            return $"{gameObject.name} - {HealthPoint}";
        }

    }
}