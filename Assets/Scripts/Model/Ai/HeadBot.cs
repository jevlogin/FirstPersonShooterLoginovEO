using System;
using UnityEngine;

namespace JevLogin
{
    public sealed class HeadBot : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChange = delegate { };
        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange.Invoke(new InfoCollision(info.Damage * 500, info.Contact, info.ObjectCollision, info.Direction));
        }
    }
}