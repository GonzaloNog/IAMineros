using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class Base : MonoBehaviour
{
    public GameObject min;
    public GameObject exp;
    public int costoMinero = 100;
    public int costExplored = 500;
    

    void Update()
    {
        
    }
    public void NewMinero()
    {
        if (GameManager.instance.GetOroTotal() >= costoMinero)
        {
            GameManager.instance.SetOroTotalMenos(costoMinero);
            min.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
            Instantiate(min);
        }
    }
    public void NewExplored()
    {
        if (GameManager.instance.GetOroTotal() >= costExplored)
        {
            GameManager.instance.SetOroTotalMenos(costExplored);
            exp.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
            Instantiate(exp);
        }
    }
    public int GetCostMinero()
    {
        return costoMinero;
    }
    public int GetCostoExplorador()
    {
        return costExplored;
    }
}
