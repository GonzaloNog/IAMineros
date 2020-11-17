using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnMinaControler spawn;
    public Base based;
    public int oroTotal = 100;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public int GetOroTotal()
    {
        return oroTotal;
    }
    public void SetOroTotalMas(int oro)
    {
        oroTotal += oro;
    }
    public void SetOroTotalMenos(int oro)
    {
        oroTotal -= oro;
    }
    public Base GetBase()
    {
        return based;
    }
    public void NuevaMina()
    {
        spawn.SetMinasOcultasMas();
    }
    public void MinaDestruida()
    {
        spawn.SetMinasOcultasMenos();
    }
    public int GetCostoExplorador()
    {
        return based.GetCostoExplorador();
    }
    public int GetCostoMinero()
    {
        return based.GetCostMinero();
    }
}
