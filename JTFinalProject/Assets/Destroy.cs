using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Destroy : MonoBehaviour
{
    // We will get 2 copies of vertices,
    // so that we could use the original positions as an input
    Vector3[] vertices;
    Vector3[] originalVertices;
    Vector3[,,] Model;
    int[] triangles;
    int Dir;//1 = Vertical 2= Horizontal 3 = Depth
    // Reference to the mesh
    Mesh mesh;
    int size;
    void Start()
    {
        var meshFilter = gameObject.GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        Dir = 1;
        // get 2 copies of vertices from an existing mesh
        vertices = mesh.vertices;
        size = vertices.Length / 3 / 3 / 3;
        Model = new Vector3[size+1, size+1, size];
        originalVertices = mesh.vertices;
        triangles = mesh.triangles;
    }
    float timer;
    int i = 0;
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= 0.5)
        {
            int x = 1;
            int y = 1;
            int z = 1;
            if (Dir == 1)
            {
                y = i;
            }
            else if (Dir == 2)
            {
                x = i;
            }
            else
            {
                z = i;
            }
            i++;
            Debug.Log(x + (size * (y + (size * z))));
            vertices[triangles[i]] /= 2;
            // Assign vertices to mesh
            mesh.vertices = vertices;
        }
    }
}
