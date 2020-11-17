using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasContro : MonoBehaviour
{
    public Text oro;
    public Text minero;
    public Text explorador;
    void Update()
    {
        oro.text = "Oro: " + GameManager.instance.GetOroTotal();
        minero.text = "" + GameManager.instance.GetCostoMinero();
        explorador.text = "" + GameManager.instance.GetCostoExplorador();
    }
}
