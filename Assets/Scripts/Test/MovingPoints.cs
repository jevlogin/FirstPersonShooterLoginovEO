using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JevLogin
{
    public sealed class MovingPoints : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Bot _agent;
        [SerializeField] private DestroyPoint _point;

        private Camera _mainCamera;
        private LineRenderer _lineRenderer;
        private readonly Queue<Vector3> _points = new Queue<Vector3>();
        private readonly Color _color1 = Color.blue;
        private readonly Color _color2 = Color.red;

        private NavMeshPath _path;
        private Vector3 _startPoint;

        #endregion


        #region Properties

        public Vector3 CurrentPoint { get; private set; }

        #endregion


        #region UnityMethods
        private void Start()

        {
            var lineRendererGo = new GameObject("LineRenderer");
            _lineRenderer = lineRendererGo.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.5f;
            _lineRenderer.endWidth = 0.2f;
            _lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
            _lineRenderer.startColor = _color1;
            _lineRenderer.endColor = _color2;

            _startPoint = _agent.transform.position;
            _path = new NavMeshPath();

            _mainCamera = GetComponent<Camera>();
            CurrentPoint = Vector3.positiveInfinity;
        }

        private void Update()
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DrawPoint(hit.point);
                }

                if (Time.deltaTime % 2 == 0)
                {
                    NavMesh.CalculatePath(_startPoint, hit.point, NavMesh.AllAreas, _path);
                    
                    var cornersArray = _points.ToArray().Concat(_path.corners);

                    _lineRenderer.positionCount = cornersArray.Length;
                    _lineRenderer.SetPositions(cornersArray);
                }
            }

            if (_points.Count <= 0)
            {
                return;
            }
            if (!_agent.Agent.hasPath)
            {
                var point = _points.Dequeue();
                _agent.MovePoint(point);
                CurrentPoint = point;
            }
        }

        private void DrawPoint(Vector3 position)
        {
            var point = Instantiate(_point, position, Quaternion.identity);
            point.OnFinishChange += MovePoint;
            _points.Enqueue(point.transform.position);
            _startPoint = point.transform.position;
        }

        private void MovePoint(GameObject obj)
        {
            if (CurrentPoint == obj.transform.position)
            {
                obj.GetComponent<DestroyPoint>().OnFinishChange -= MovePoint;
                CustomDebug.IsDebug = true;
                CustomDebug.Log("Удаляем точку");
                CustomDebug.IsDebug = false;

                Destroy(obj);
            }
        }
        #endregion
    }
}
