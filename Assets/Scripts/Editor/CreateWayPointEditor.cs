using System;
using UnityEditor;
using UnityEngine;

namespace JevLogin
{
    [CustomEditor(typeof(CreateWayPoint))]
    public sealed class CreateWayPointEditor : Editor
    {
        private CreateWayPoint _testTarget;

        private void OnEnable()
        {
            _testTarget = (CreateWayPoint)target;
        }

        private void OnSceneGUI()
        {
            if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
            {
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
                    SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

                if (Physics.Raycast(ray, out var hit))
                {
                    _testTarget.InstantiateObject(hit.point);
                    SetObjectDirty(_testTarget.gameObject);
                }
            }
        }

        private void SetObjectDirty(GameObject gameObject)
        {

        }
    }
}
