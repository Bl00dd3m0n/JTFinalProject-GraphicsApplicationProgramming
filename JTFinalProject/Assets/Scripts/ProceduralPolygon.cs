using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralPolygon : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> verticies;
    List<int> triangles;
    public int faces;
    public int faceSize;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeCube();
        UpdateMesh();
    }
    void GenerateVerticies(int dir)
    {

        verticies = new List<Vector3>();

        //set tracker integers
        int v = 0;
        float vertexOffset = faceSize * 0.5f;
        for (int x = 0; x < faces; x++)
        {
            for (int y = 0; y < faces; y++)
            {
                Vector3 cellOffset = new Vector3(x * faceSize, 0, y * faceSize);
                verticies.Add(new Vector3(-vertexOffset, dir, -vertexOffset) + cellOffset);
                verticies.Add(new Vector3(-vertexOffset, dir, vertexOffset) + cellOffset);
                verticies.Add(new Vector3(vertexOffset, dir, -vertexOffset) + cellOffset);
                verticies.Add(new Vector3(vertexOffset, dir, vertexOffset) + cellOffset);
                v += 4;
            }
        }
    }
    void MakeCube()
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            MakeFace(i);
        }
    }

    void MakeFace(int dir)
    {
        verticies.AddRange(PolygonMeshData.faceVerticies(dir));
        //GenerateVerticies(dir);
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
