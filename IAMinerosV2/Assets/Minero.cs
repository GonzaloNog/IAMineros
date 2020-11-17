using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minero : MonoBehaviour
{
    public float speed = 30.0f;
    public int CargaOroMax = 10;
    private string state;
    private bool startbool = false;
    private int currentTargetIndex;
    private bool ObjetivoTemporal = false;
    private List<Vector3> targetList;
    private GameObject targetMina;
    public int oro = 0;
    public bool dig = false;
    
    void Start()
    {
        state = "idle";
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "idle":
                if (!startbool)
                    StartCoroutine(StartMinero());
                break;
            case "patrol":
                if (!ObjetivoTemporal)
                    GetObjective();
                HandleMovement();
                break;
            case "mining":
                Minar();
                break;
            case "returning":
                Retornar();
                break;
        }
    }
    IEnumerator StartMinero()
    {
        startbool = true;
        yield return new WaitForSeconds(1.0f);
        state = "patrol";
        yield return null;
    }
    IEnumerator Gold()
    {
        dig = true;
        yield return new WaitForSeconds(0.5f);
        oro += targetMina.GetComponent<Mina>().RobarOro(5);
        dig = false;
        yield return null;
    }
    private void Retornar()
    {
        Pathfinding.Instance.GetGrid().GetXY(new Vector3(GameManager.instance.GetBase().transform.position.x, GameManager.instance.GetBase().transform.position.y, 5), out int x, out int y);
        this.SetTargetPosition(Pathfinding.Instance.GetGrid().GetWorldPosition(x, y));
        ObjetivoTemporal = true;
        HandleMovement();
        if (!ObjetivoTemporal)
        {
            GameManager.instance.SetOroTotalMas(oro);
            oro = 0;
            if (targetMina != null)
            {
                startbool = false;
                state = "mining";
            }  
            else
                state = "idle";
        }
    }
    private void Minar()
    {
        if (targetMina != null)
        {
            Pathfinding.Instance.GetGrid().GetXY(new Vector3(targetMina.transform.position.x, targetMina.transform.position.y, 5), out int x, out int y);
            this.SetTargetPosition(Pathfinding.Instance.GetGrid().GetWorldPosition(x, y));
            ObjetivoTemporal = true;
            HandleMovement();
        }
        else
            state = "returning";
            
        if (!ObjetivoTemporal && targetMina != null)
        {
            if (!dig && oro < CargaOroMax)
                StartCoroutine(Gold());
        }
        if (oro >= CargaOroMax)
            state = "returning";
    }
    private void HandleMovement()
    {
        if (targetList != null)
        {
            Vector3 targetPosition = targetList[currentTargetIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentTargetIndex++;
                if (currentTargetIndex >= targetList.Count)
                {
                    StopMoving();
                    ObjetivoTemporal = false;
                    if (state == "patrol")
                    {
                        state = "idle";
                        startbool = false;
                    }
                }
            }
        }
        else
        {

        }
    }

    private void StopMoving()
    {
        targetList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void GetObjective()
    {
        //print("ENTRO");
        int X;
        int Y;
        
        X = Random.Range(0, 20) * 10;
        Y = Random.Range(0, 10) * 10;
        Pathfinding.Instance.GetGrid().GetXY(new Vector3(X, Y, 5), out int x, out int y);
        this.SetTargetPosition(Pathfinding.Instance.GetGrid().GetWorldPosition(x, y));
        //Debug.Log(Pathfinding.Instance.GetNode(X, Y).GetWalkable());
        ObjetivoTemporal = true;
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentTargetIndex = 0;
        targetList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (targetList != null && targetList.Count > 1)
        {
            targetList.RemoveAt(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("Coliciono");
        Mina min = col.GetComponent<Mina>();
        if (col.gameObject.tag == "Mina" && min.GetDescubierta())
        {
            //Debug.Log("Coliciono");
            if (state == "patrol")
            {  
                targetMina = col.gameObject;
                StopMoving();
                ObjetivoTemporal = false;
                state = "mining";
            }
        }
    }

}
