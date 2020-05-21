using UnityEngine;


namespace JevLogin
{
    public sealed class CreateWayPoint : MonoBehaviour
    {
        [SerializeField] private DestroyPoint _prefab;
        private PathBot _rootWayPoint;

        public void InstantiateObject(Vector3 position)
        {
            if (!_rootWayPoint)
            {
                _rootWayPoint = new GameObject("WayPoint").AddComponent<PathBot>();
            }

            if (_prefab != null)
            {
                Instantiate(_prefab, position, Quaternion.identity, _rootWayPoint.transform);
            }
            else
            {
                throw new System.Exception($"Нет префаба на компоненте {typeof(CreateWayPoint)} {gameObject.name}");
            }
        }
    }
}
