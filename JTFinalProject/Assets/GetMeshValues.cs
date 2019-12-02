using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMeshValues : MonoBehaviour
{
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        foreach (Vector3 vec3 in mesh.vertices)
        {
            Debug.Log(vec3);
        }
        foreach (int triangle in mesh.triangles)
        {
            Debug.Log(triangle);
        }

    }
    float timer;
    List<Vector3> tempVerticies = new List<Vector3>();
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            foreach(Vector3 vec3 in mesh.vertices)
            {
                tempVerticies.Add(new Vector3(vec3.x-0.1f,vec3.y-0.1f,vec3.z));
            }
            Debug.Log(tempVerticies[tempVerticies.Count - 1]);
            mesh.vertices = tempVerticies.ToArray();
            tempVerticies.Clear();
            timer = 0;
        }
    }
}
