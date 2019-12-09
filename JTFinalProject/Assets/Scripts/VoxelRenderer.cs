using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRenderer : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> verticies;
    List<int> triangles;
    public int height;
    public int width;
    public float scale = 1f;

    float adjustedScale;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateVoxelMesh( new VoxelData() );
        UpdateMesh();
    }
    
    void GenerateVoxelMesh(VoxelData data)
    {
        verticies = new List<Vector3>();
        triangles = new List<int>();
        for (int z = 0; z < data.Depth; z++)
        {

            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    if (data.GetCell(x, y, z) == 0)
                    {
                        continue;
                    }
                    MakeCube(adjustedScale, new Vector3((float)x * scale, (float)y * scale, (float)z * scale), x, y, z, data);
                }
            }
        }
    }

    void MakeCube(float cubeScale, Vector3 cubePos,int x, int y, int z, VoxelData data)
    {
        for (int i = 0; i < 6; i++)
        {
            if(data.GetNeighbor(x,y,z,(Direction)i) == 0)
            {
                MakeFace((Direction)i, cubeScale, cubePos);
            } 
        }
    }

    void MakeFace(Direction dir, float faceScale, Vector3 facePos)
    {
        verticies.AddRange(CubeMeshData.faceVerticies(dir, faceScale, facePos));
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
        if (Input.GetKeyDown(KeyCode.U))
        {
            VoxelData data = new VoxelData();
            data.GenerateCone(width,height);
            GenerateVoxelMesh(data);
            
            UpdateMesh();
        }
    }
}
