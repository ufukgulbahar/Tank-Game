using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    Transform player;
    NavMeshAgent agent;
    public Transform p1, p2, p3;
    public Transform rayOrigin;
    int Index = 0;
    Animator fsm;
    Vector3[] wayPoints;
    float shootFreq = 5f;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = new Vector3[] { p1.position, p2.position,p3.position };

      
        fsm = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(wayPoints[Index]);


    }

    void FixedUpdate()
    {


        float distance = Vector3.Distance(player.position, transform.position);
        fsm.SetFloat("distance", distance);



        Vector3 direction = player.position - rayOrigin.position;
        direction = direction.normalized;


        float distanceFromWayPoint = Vector3.Distance(transform.position, wayPoints[Index]);
        fsm.SetFloat("distanceFromWaypoint", distanceFromWayPoint);

        Debug.DrawRay(rayOrigin.position, direction * 20, Color.red);


        if (Physics.Raycast(rayOrigin.position, direction, out RaycastHit info, 20))
        {
            if (info.transform.CompareTag("Player"))
                fsm.SetBool("isVisible", true);
            else
                fsm.SetBool("isVisible", false);
        }
        else
            fsm.SetBool("isVisible", false);
    }


    public void SetLookRotation()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.2f);
    }

    public void Shoot()
    {
        GetComponent<ShootBeHaviour>().Shoot(shootFreq);
    }

    public void Patrol()
    {
       
      
    }

    public void Chase()
    {
        agent.SetDestination(player.position);
    }


    public void SetNextWayPoint()
    {   
        switch (Index)
        {
            case 0:
                Index = 1;
                break;
            case 1:
                Index = 2;
                break;
            case 2:
                Index = 0;
                break;
        }

        agent.SetDestination(wayPoints[Index]);
    }




}


