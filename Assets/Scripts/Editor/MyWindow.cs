using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;


namespace JevLogin
{
    public sealed class MyWindow : EditorWindow
    {
        #region Fields

        public static GameObject ObjectInstantiate;

        public string NameObject = "Hello World";
        public float Radius = 10.0f;
        public int CountObject = 1;
        public bool GroupEnabled;
        public bool RandomColor = true;

        #endregion

        private void OnGUI()
        {
            GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
            ObjectInstantiate = EditorGUILayout.ObjectField("Оюъект который хотим вставить", ObjectInstantiate, typeof(GameObject), true) as GameObject;
            NameObject = EditorGUILayout.TextField("Имя объекта", NameObject);
            GroupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки", GroupEnabled);
            RandomColor = EditorGUILayout.Toggle("Случайный цвет", RandomColor);
            CountObject = EditorGUILayout.IntSlider("Количество объектов", CountObject, 1, 100);
            Radius = EditorGUILayout.Slider("Радиус окружности", Radius, 1, 100);
            EditorGUILayout.EndToggleGroup();
            if (GUILayout.Button("Создать объекты"))
            {
                if (ObjectInstantiate)
                {
                    GameObject root = new GameObject("Root");
                    for (int i = 0; i < CountObject; i++)
                    {
                        float angle = i * Mathf.PI * 2 / CountObject;
                        Vector3 position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * Radius;
                        GameObject temp = Instantiate(ObjectInstantiate, position, Quaternion.identity);
                        temp.name = $"{NameObject} ({i})";
                        temp.transform.parent = root.transform;
                        var tempRenderer = temp.GetComponent<Renderer>();
                        if (tempRenderer && RandomColor)
                        {
                            tempRenderer.material.color = Random.ColorHSV();
                        }
                    }
                }
            }
        }
    }
}