using System;
using UnityEngine;


namespace JevLogin
{
    [RequireComponent(typeof(Renderer)), ExecuteInEditMode, DisallowMultipleComponent]
    public class TestAttribute : MonoBehaviour
    {
        [HideInInspector] public float TestHidePublic;
        [SerializeField] private float _testSerializePrivate = 7.0f;
        private float _testPrivateTwo;
        public SerializableGameObject SerializableGameObject;

        private const int _min = 0;
        private const int _max = 100;
        [Header("Test variables")]
        [ContextMenuItem("Randomize Number", nameof(Randomize))]
        [Range(_min, _max)]
        public int SecondTest;

        

        [Space(60)]
        [SerializeField, Multiline(5)] private string _testMultiline;
        [Space(60)]
        [SerializeField, TextArea(5, 5), Tooltip("Tooltip text")] private string _testArea;

        private void Update()
        {
            GetComponent<Renderer>().sharedMaterial.color = UnityEngine.Random.ColorHSV();
        }

        private void Randomize()
        {
            SecondTest = UnityEngine.Random.Range(_min, _max);
        }

        [Obsolete("Устарело. Используй что-то другое")]
        private void TestObsolete()
        {

        }

        private void OnGUI()
        {
            GUILayout.Button("Click me");
        }
    }
}
