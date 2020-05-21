using UnityEditor;
using UnityEngine;

namespace JevLogin
{
    [CustomEditor(typeof(TestBehaviour))]
    public class TestBehaviourEditor : Editor
    {
        private bool _isPressButtonOk;

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();
            TestBehaviour testTarget = (TestBehaviour)target;

            testTarget.Count = EditorGUILayout.IntSlider(testTarget.Count, 10, 50);
            testTarget.Offset = EditorGUILayout.IntSlider(testTarget.Offset, 1, 5);

            testTarget.EnemyObject = EditorGUILayout.ObjectField("Объект, который хотим вставить",
                testTarget.EnemyObject, typeof(GameObject), false) as GameObject;

            var isPressButton = GUILayout.Button("Создание объектов по кнопке", EditorStyles.miniButtonLeft);

            _isPressButtonOk = GUILayout.Toggle(_isPressButtonOk, "Ok");

            if (isPressButton)
            {
                testTarget.CreateEnemyObject();
                _isPressButtonOk = true;
            }

            if (_isPressButtonOk)
            {
                testTarget.Test = EditorGUILayout.Slider(testTarget.Test, 10, 50);
                EditorGUILayout.HelpBox("Вы нажали на кнопку", MessageType.Warning);

                var isPressAddButton = GUILayout.Button("Add Component", EditorStyles.miniButtonLeft);
                if (isPressAddButton)
                {
                    testTarget.AddComponent();
                }
                if (GUILayout.Button("Remove Component", EditorStyles.miniButtonLeft))
                {
                    testTarget.RemoveComponent();
                }
            }
        }
    }
}
