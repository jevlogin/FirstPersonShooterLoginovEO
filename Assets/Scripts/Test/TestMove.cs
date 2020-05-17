using UnityEngine;


namespace JevLogin
{
    public sealed class TestMove : MonoBehaviour
    {
        #region Fields

        public Transform TargetTransform;

        #endregion


        #region Properties

        public UnityEngine.AI.NavMeshAgent Agent { get; private set; }

        #endregion


        #region UnityMethods

        private void Start()
        {
            Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        private void Update()
        {
            if (TargetTransform != null)
            {
                Agent.SetDestination(TargetTransform.position);
            }
        }

        #endregion
    }
}
