using System;
using UnityEngine;


namespace JevLogin
{
    public sealed class BodyBot : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChange = delegate { };
        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange.Invoke(info);
        }
    }
}