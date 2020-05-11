using UnityEngine;

namespace JevLogin
{
    public sealed class Bullet : Ammunition
    {
        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }
    }
}