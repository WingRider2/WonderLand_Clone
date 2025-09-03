using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class InvertNormals : MonoBehaviour
{
    void Start()
    {
        var mf = GetComponent<MeshFilter>();
        var mesh = mf.mesh;

        // 1) 노말 뒤집기
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -normals[i];
        mesh.normals = normals;

        // 2) 삼각형 winding 뒤집기
        for (int s = 0; s < mesh.subMeshCount; s++)
        {
            int[] tris = mesh.GetTriangles(s);
            for (int i = 0; i < tris.Length; i += 3)
            {
                int tmp = tris[i];
                tris[i] = tris[i + 1];
                tris[i + 1] = tmp;
            }
            mesh.SetTriangles(tris, s);
        }
    }
}
