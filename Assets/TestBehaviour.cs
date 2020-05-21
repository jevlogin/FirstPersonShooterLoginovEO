using System;
using UnityEngine;


namespace JevLogin
{
    public sealed class TestBehaviour : MonoBehaviour
    {
        #region Fields

        private Transform _root;

        public GameObject EnemyObject;

        public int Count = 10;
        public int Offset = 1;
        public float Test;

        #endregion

        private void Awake()
        {
            CustomDebug.Log(3456546);
        }

        private void Start()
        {
            CreateEnemyObject();
        }

        private void CreateEnemyObject()
        {
            _root = new GameObject("Root").transform;
            for (var i = 1; i <= Count; i++)
            {
                Instantiate(EnemyObject, new Vector3(0, Offset * i, 0), Quaternion.identity, _root);
                //TODO Свой алгоритм по расстановке мин или аптечек
            }
        }

        public void AddComponent()
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<MeshRenderer>();
            gameObject.AddComponent<BoxCollider>();
        }

        public void RemoveComponent()
        {
            DestroyImmediate(GetComponent<Rigidbody>());
            DestroyImmediate(GetComponent<MeshRenderer>());
            DestroyImmediate(GetComponent<BoxCollider>());
        }

        private void OnGUI()
        {
            GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 20), "Click me");
        }
    }
}
