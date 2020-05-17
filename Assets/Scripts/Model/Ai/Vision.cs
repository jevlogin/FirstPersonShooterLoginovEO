using UnityEngine;


namespace JevLogin
{
    [System.Serializable]
    public sealed class Vision
    {
        #region Fields

        public float ActiveDistance = 10.0f;
        public float ActiveAngle = 35.0f;

        #endregion


        #region Methods

        public bool VisionMinimumDistance(Transform player, Transform target)
        {
            return Distance(player, target) && Angle(player, target) && !CheckBloked(player, target);
        }

        private bool CheckBloked(Transform player, Transform target)
        {
            if (!Physics.Linecast(player.position, target.position, out var hit))
            {
                return true;
            }
            return hit.transform != target;
        }

        private bool Angle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(target.position - player.position, player.forward);
            return angle <= ActiveAngle;
        }

        private bool Distance(Transform player, Transform target)
        {
            return (player.position - target.position).sqrMagnitude <= ActiveDistance * ActiveAngle;
        }

        #endregion
    }
}
