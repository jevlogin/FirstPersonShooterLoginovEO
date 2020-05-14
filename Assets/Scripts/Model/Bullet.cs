using UnityEngine;


namespace JevLogin
{
    public sealed class Bullet : Ammunition
    {
        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.OnCollision(new InfoCollision(_curDamage, collision.contacts[0], collision.transform, Rigidbody.velocity));
            }

            DestroyAmmunition();
        } 

        #endregion
    }
}