using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TestCool : MonoBehaviour
{
    [SerializeField] private HeatMapVisual heatMapVisual;
    private Grid grid;
    private void Start()
    {
        //grid = new Grid(100,100,3f, this.transform.position);

       // heatMapVisual.SetGrid(grid);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            //grid.AddValue(position, 100, 5, 30);
        }

        if (Input.GetMouseButtonDown(1))
        {
            
        }
    }
}
