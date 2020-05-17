using System;
using UnityEngine;


namespace JevLogin
{
    public class BulletProjector : MonoBehaviour
    {
        #region Fields

        [SerializeField] private float _distanceToLerance = 0.1f;

        private Projector _projector;

        private float _origNearClipPlane;
        private float _origFarClipPlane;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _projector = GetComponent<Projector>();
            _origNearClipPlane = _projector.nearClipPlane;
            _origFarClipPlane = _projector.farClipPlane;
            Late();
        }

        #endregion


        #region Methods

        private void Late()
        {
            var ray = new Ray(_projector.transform.position + _projector.transform.forward.normalized * _origNearClipPlane,
                _projector.transform.forward);
            if (!Physics.Raycast(ray, out var hit, _origFarClipPlane - _origNearClipPlane, ~_projector.ignoreLayers))
            {
                return;
            }
            var distance = hit.distance + _origNearClipPlane;
            _projector.nearClipPlane = Mathf.Max(distance - _distanceToLerance, 0);
            _projector.farClipPlane = distance + _distanceToLerance;
        } 

        #endregion
    }
}
