using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> verticies;
    List<int> triangles;

    public float scale = 1f;
    public int posx, posy, posz;

    float adjustedScale;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeCube(adjustedScale, new Vector3((float)posx * scale, (float)posy * scale, (float)posz * scale));
        UpdateMesh();
    }

    void MakeCube(float cubeScale, Vector3 cubePos)
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            MakeFace(i, cubeScale, cubePos);
        }
    }

    void MakeFace(int dir, float faceScale, Vector3 facePos)
    {
        verticies.AddRange(CubeMeshData.faceVerticies(dir,faceScale,facePos));
        int vCount = verticies.Count;
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
