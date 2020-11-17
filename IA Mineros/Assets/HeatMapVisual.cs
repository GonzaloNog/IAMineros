using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class HeatMapVisual : MonoBehaviour
{
    
    private Grid<int> grid;
    private Mesh mesh;
    private bool updateControl;
    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    public void SetGrid(Grid<int> grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();

        //grid.OnGridValueChanged += Grid_OnGridValueChange;
    }
    
    private void Grid_OnGridValueChange(object sender, Grid<int>.OnGridObjectChangedEventArgs e)
    {
        updateControl = true;
    }
    private void LateUpdate()
    {
        if (updateControl)
        {
            updateControl = false;
            UpdateHeatMapVisual();
        }
    }

    private void UpdateHeatMapVisual()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);
        
        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1,1) * grid.GetCellSize();

                int gridValue = grid.GetGridObject(x, y);
                //float gridValueNormalizes = (float)gridValue / Grid<int>.HEAT_MAP_MAX_VALUE;
               // Vector2 gridValueUV = new Vector2(gridValueNormalizes, 0f);
               // MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f,0.0f, quadSize,gridValueUV,gridValueUV);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
