using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMinaControler : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public Mina min;

    private int minasOcultas = 0;
    void Update()
    {
        var aux = Random.Range(0, SpawnPoints.Length - 1);
        if (minasOcultas < 3)
        {
            min.transform.position = SpawnPoints[aux].transform.position;
            Instantiate(min);
            minasOcultas++;
        }
    }
    public void SetMinasOcultasMenos()
    {
        minasOcultas--;
        if (minasOcultas < 0)
            minasOcultas = 0;
    }
    public void SetMinasOcultasMas()
    {
        minasOcultas++;
    }
}
