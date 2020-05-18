using UnityEngine;
using UnityEngine.AI;


namespace JevLogin
{
    public static class Patrol
    {
        #region Methods

        public static Vector3 GenericPoint(Transform agent)
        {
            //todo перемещение по точкам
            Vector3 result;

            //todo если плохая точка, то перебросить
            var maxDistance = Random.Range(5, 50);
            var randomPoint = Random.insideUnitSphere * maxDistance;

            NavMesh.SamplePosition(agent.position + randomPoint, out var hit, maxDistance, NavMesh.AllAreas);

            result = hit.position;

            return result;
        }

        #endregion
    }
}