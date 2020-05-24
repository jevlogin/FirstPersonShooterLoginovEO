using UnityEditor;
using UnityEngine;


namespace JevLogin
{
    public class CreateCone : ScriptableWizard
    {
        #region Fields

        [SerializeField]
        public int NumVertices = 10;
        public float RadiusTop = 0.0f;
        public float RadiusBottom = 1.0f;
        public float Length = 1.0f;
        public float OpeningAngle = 0.0f; // if >0, create a cone with this angle by setting radiusTop to 0, and adjust radiusBottom according to length;
        public bool Outside = true;
        public bool Inside = false;
        public bool AddCollider = false;

        #endregion


        [MenuItem("GameObject/Create Other/Cone")]
        private static void CreateWizard()
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
                Vector2[] uvs = new Vector2[2 * multiplier * NumVertices];
                int[] tris;
                float slope = Mathf.Atan((RadiusBottom - RadiusTop) / Length); // (rad difference)/height
                float slopeSin = Mathf.Sin(slope);
                float slopeCos = Mathf.Cos(slope);
                int i;

                for (i = 0; i < NumVertices; i++)
                {
                    float angle = 2 * Mathf.PI * i / NumVertices;
                    float angleSin = Mathf.Sin(angle);
                    float angleCos = Mathf.Cos(angle);
                    float angleHalf = 2 * Mathf.PI * (i * 0.5f) / NumVertices; // for degenerated normals at cone tips
                    float angleHalfSin = Mathf.Sin(angleHalf);
                    float angleHalfCos = Mathf.Cos(angleHalf);

                    vertices[i] = new Vector3(RadiusTop * angleCos, RadiusTop * angleSin, 0);
                    vertices[i + NumVertices] = new Vector3(RadiusBottom * angleCos, RadiusBottom * angleSin, Length);

                    if (RadiusTop == 0)
                    {
                        normals[i] = new Vector3(angleHalfCos * slopeCos, angleHalfSin * slopeCos, -slopeSin);
                    }
                    else
                    {
                        normals[i] = new Vector3(angleCos * slopeCos, angleSin * slopeCos, -slopeSin);
                    }

                    if (RadiusBottom == 0)
                    {
                        normals[i + NumVertices] = new Vector3(angleHalfCos * slopeCos, angleHalfSin * slopeCos, -slopeSin);
                    }
                    else
                    {
                        normals[i + NumVertices] = new Vector3(angleCos * slopeCos, angleSin * slopeCos, -slopeSin);
                    }

                    uvs[i] = new Vector2(1.0f * i / NumVertices, 1);
                    uvs[i + NumVertices] = new Vector2(1.0f * i / NumVertices, 0);

                    if (Outside && Inside)
                    {
                        // vertices and uvs are identical on inside and outside, so just copy
                        vertices[i + 2 * NumVertices] = vertices[i];
                        vertices[i + 3 * NumVertices] = vertices[i + NumVertices];
                        uvs[i + 2 * NumVertices] = uvs[i];
                        uvs[i + 3 * NumVertices] = uvs[i + NumVertices];
                    }

                    if (Inside)
                    {
                        //  invert normals
                        normals[i + offset] = -normals[i];
                        normals[i + NumVertices + offset] = -normals[i + NumVertices];
                    }
                }
                mesh.vertices = vertices;
                mesh.normals = normals;
                mesh.uv = uvs;

                // create triangles
                // here we need to take care of point order, depending on inside and outside
                int cnt = 0;
                if (RadiusTop == 0)
                {
                    //  top cone
                    tris = new int[NumVertices * 3 * multiplier];
                    if (Outside)
                    {
                        for (i = 0; i < NumVertices; i++)
                        {
                            tris[cnt++] = i + NumVertices;
                            tris[cnt++] = i;
                            if (i == NumVertices - 1)
                            {
                                tris[cnt++] = NumVertices;
                            }
                            else
                            {
                                tris[cnt++] = i + 1 + NumVertices;
                            }
                        }
                    }
                    if (Inside)
                    {
                        for (i = offset; i < NumVertices; i++)
                        {
                            tris[cnt++] = i;
                            tris[cnt++] = i + NumVertices;
                            if (i == NumVertices - 1 + offset)
                            {
                                tris[cnt++] = NumVertices + offset;
                            }
                            else
                            {
                                tris[cnt++] = i + 1 + NumVertices;
                            }
                        }
                    }
                }
                else if (RadiusBottom == 0)
                {
                    //  bottom cone
                    tris = new int[NumVertices * 3 * multiplier];
                    if (Outside)
                    {
                        for (i = 0; i < NumVertices; i++)
                        {
                            tris[cnt++] = i;
                            if (i == NumVertices - 1)
                            {
                                tris[cnt++] = 0;
                            }
                            else
                            {
                                tris[cnt++] = i + 1;
                            }
                            tris[cnt++] = i + NumVertices;
                        }
                    }
                    if (Inside)
                    {
                        for (i = offset; i < NumVertices + offset; i++)
                        {
                            if (i == NumVertices - 1 + offset)
                            {
                                tris[cnt++] = offset;
                            }
                            else
                            {
                                tris[cnt++] = i + 1;
                            }
                            tris[cnt++] = i;
                            tris[cnt++] = i + NumVertices;
                        }
                    }
                }
                else
                {
                    // truncated cone
                    tris = new int[NumVertices * 6 * multiplier];
                    if (Outside)
                    {
                        for (i = 0; i < NumVertices; i++)
                        {
                            int ip1 = i + 1;
                            if (ip1 == NumVertices)
                            {
                                ip1 = 0;
                            }

                            tris[cnt++] = i;
                            tris[cnt++] = ip1;
                            tris[cnt++] = i + NumVertices;

                            tris[cnt++] = ip1 + NumVertices;
                            tris[cnt++] = i + NumVertices;
                            tris[cnt++] = ip1;
                        }
                    }
                    if (Inside)
                    {
                        for (i = offset; i < NumVertices + offset; i++)
                        {
                            int ip1 = i + 1;
                            if (ip1 == NumVertices + offset)
                            {
                                ip1 = offset;
                            }

                            tris[cnt++] = ip1;
                            tris[cnt++] = i;
                            tris[cnt++] = i + NumVertices;

                            tris[cnt++] = i + NumVertices;
                            tris[cnt++] = ip1 + NumVertices;
                            tris[cnt++] = ip1;
                        }
                    }
                }
                mesh.triangles = tris;
                AssetDatabase.CreateAsset(mesh, meshPrefabPath);
                AssetDatabase.SaveAssets();
            }

            MeshFilter mf = newCone.AddComponent<MeshFilter>();
            mf.mesh = mesh;

            newCone.AddComponent<MeshRenderer>();

            if (AddCollider)
            {
                MeshCollider mc = newCone.AddComponent<MeshCollider>();
                mc.sharedMesh = mf.sharedMesh;
            }

            Selection.activeObject = newCone;

        }
    }
}