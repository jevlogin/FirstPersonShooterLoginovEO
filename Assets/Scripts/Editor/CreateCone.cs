using UnityEditor;
using UnityEngine;


namespace JevLogin
{
    public class CreateCone : ScriptableWizard
    {
        [SerializeField]
        public int NumVertices = 10;
        public float RadiusTop = 0.0f;
        public float RadiusBottom = 1.0f;
        public float Length = 1.0f;
        public float OpeningAngle = 0.0f;
        public bool Outside = true;
        public bool Inside = false;
        public bool AddCollide = false;

        [MenuItem("GameObject/Create Other/Cone")]
        private void CreateWizzard()
        {
            ScriptableWizard.DisplayWizard("Create Cone", typeof(CreateCone));
        }

        private void OnWizardCreate()
        {
            GameObject newCone = new GameObject("Cone");
            if (OpeningAngle > 0 && OpeningAngle < 180)
            {
                RadiusTop = 0;
                RadiusBottom = Length * Mathf.Tan(OpeningAngle * Mathf.Deg2Rad / 2);
            }
            string meshName = newCone.name + NumVertices + "v" + RadiusTop + "t" + RadiusBottom + "b" + Length + "l" + Length + (Outside ? "o" : "") + (Inside ? "i" : "");
            string meshPrefabPath = "Assets/Editor/" + meshName + ".asset";
            Mesh mesh = (Mesh)AssetDatabase.LoadAssetAtPath(meshPrefabPath, typeof(Mesh));
            if (mesh == null)
            {
                mesh = new Mesh();
                mesh.name = meshName;
                // can't acces Camera.current
                // newCone.transform.position = Camera.current.position + Camera.current.transform.forward * 5.0f;
                int multiplier = (Outside ? 1 : 0) + (Inside ? 1 : 0);
                int offset = (Outside && Inside ? 2 * NumVertices : 0);
                Vector3[] vertices = new Vector3[2 * multiplier * NumVertices];
                Vector3[] normals = new Vector3[2 * multiplier * NumVertices];
                Vector3[] uvs = new Vector3[2 * multiplier * NumVertices];
                int[] tris;

            }
        }
    }
}