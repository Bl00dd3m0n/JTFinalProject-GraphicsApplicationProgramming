using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    Mesh mesh;
    Vector3[] verticies;
    int[] triangles;

    //grid settings
    public float cellSize = 1;
    public Vector3 gridOffset;
    public int gridSize;
    public Button addButton;
    public Button removeButton;
    public Text CellsText;
    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeDiscreteProceduralGrid();
        UpdateMesh();
        addButton.onClick.AddListener(AddCells); 
        removeButton.onClick.AddListener(RemoveCells);
    }

    public void AddCells()
    {
        gridSize += 1;
        MakeDiscreteProceduralGrid();
        Debug.Log("test");
        UpdateMesh();
    }
    public void RemoveCells()
    {
        if (gridSize > 1)
        {
            gridSize -= 1;
        }
        MakeDiscreteProceduralGrid();
        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        CellsText.text = "Quad Cells" + gridSize;
    }

    void MakeDiscreteProceduralGrid()
    {
        Debug.Log("Check");
        //set array sizes
        verticies = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker integers
        int v = 0;
        int t = 0;
        float vertexOffset = cellSize * 0.5f;
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0;y < gridSize; y++) {
                Vector3 cellOffset = new Vector3(x* cellSize, 0, y*cellSize);
                verticies[v] = new Vector3(-vertexOffset, x+y, -vertexOffset)  + cellOffset + gridOffset;
                verticies[v+1] = new Vector3(-vertexOffset, x+y, vertexOffset) + cellOffset + gridOffset;
                verticies[v+2] = new Vector3(vertexOffset, x+y, -vertexOffset) + cellOffset + gridOffset;
                verticies[v+3] = new Vector3(vertexOffset, x+y, vertexOffset) + cellOffset + gridOffset;
                triangles[t+0] = v;
                triangles[t+1] = triangles[t+4] = v+1;
                triangles[t+2] = triangles[t+3] = v+2;
                triangles[t+5] = v+3;

                v += 4;
                t += 6;
            }
        }

        //set vertex offset

        //populate the verticies and triangles arrays
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
