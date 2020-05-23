using System.Linq;
using UnityEngine;


namespace JevLogin
{
    public sealed class PathBot : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Color _lineColor = Color.red;

        private Vector3[] _nodes;

        #endregion


        #region UnityMethods

        private void OnValidate()
        {
            _nodes = GetComponentsInChildren<Transform>().Where(t => t != transform).Select(t => t.position).ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _lineColor;
            for (var i = 0; i < _nodes.Length; i++)
            {
                var currentNode = _nodes[i];
                var previousNode = Vector3.zero;
                if (i > 0)
                {
                    previousNode = _nodes[i - 1];
                }
                else if (i == 0 && _nodes.Length > 1)
                {
                    previousNode = _nodes[_nodes.Length - 1];
                }
                Gizmos.DrawLine(previousNode, currentNode);
                Gizmos.DrawWireSphere(currentNode, 0.3f);
            }
        }

        #endregion
    }
}