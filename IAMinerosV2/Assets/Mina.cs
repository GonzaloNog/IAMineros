using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour
{
    public int oro = 20;
    public bool descubierta = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oro <= 0)
        {
           // Debug.Log("EXPLOTO");
            Destroy(gameObject);
        }
    }
    public int GetOro()
    {
        return oro;
    }
    public void SetOro(int _oro)
    {
        oro = _oro;
    }
    public bool GetDescubierta()
    {
        return descubierta;
    }
    public void SetDescubierta(bool _des)
    {
        descubierta = _des;
        if (descubierta == true)
            GameManager.instance.MinaDestruida();
    }
    public int RobarOro(int robo)
    {
        int aux;
        if (robo < oro)
            aux = robo;
        else
            aux = oro;
        oro -= robo;
        if (oro < 0)
            oro = 0;
        return aux;
    }
}
