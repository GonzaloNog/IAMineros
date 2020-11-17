﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorador : MonoBehaviour
{
    public float speed = 30.0f;
    private string state;
    private bool startbool = false;
    private int currentTargetIndex;
    private bool ObjetivoTemporal = false;
    private List<Vector3> targetList;
    private GameObject targetMina;

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
            case "marking":
                Marking();
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

   
    private void Marking()
    {
        if (Vector3.Distance(transform.position, targetMina.transform.position) > 10f)
        {
            Pathfinding.Instance.GetGrid().GetXY(new Vector3(targetMina.transform.position.x, targetMina.transform.position.y, 5), out int x, out int y);
            this.SetTargetPosition(Pathfinding.Instance.GetGrid().GetWorldPosition(x, y));
            ObjetivoTemporal = true;
            HandleMovement();
        }
        else
        {
            targetMina.GetComponent<Mina>().SetDescubierta(true);
            startbool = false;
            state = "idle";
            ObjetivoTemporal = false;
        }
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
        int X = Random.Range(0, 20) * 10;
        int Y = Random.Range(0, 10) * 10;

        Pathfinding.Instance.GetGrid().GetXY(new Vector3(X, Y, 5), out int x, out int y);
        this.SetTargetPosition(Pathfinding.Instance.GetGrid().GetWorldPosition(x, y));
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
        if (col.gameObject.tag == "Mina" && !min.GetDescubierta())
        {
            //Debug.Log("Coliciono");
            if (state == "patrol")
            {
                targetMina = col.gameObject;
                StopMoving();
                ObjetivoTemporal = false;
                state = "marking";
            }
        }
    }
}