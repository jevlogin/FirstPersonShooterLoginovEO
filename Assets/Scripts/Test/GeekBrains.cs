using UnityEngine;


namespace JevLogin
{
    public sealed class GeekBrains : MonoBehaviour
    {
        [SerializeField] private bool _isAllowScalling;
        [SerializeField] private float _activeDistance;

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position, "bot.png", _isAllowScalling);
        }

        private void OnDrawGizmosSelected()
        {
            Transform t = transform;


            var flat = new Vector3(_activeDistance, 0, _activeDistance);
            Gizmos.matrix = Matrix4x4.TRS(t.position, t.rotation, flat);
            //Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.DrawWireSphere(Vector3.zero, 5);
        }

#endif
    }
}
