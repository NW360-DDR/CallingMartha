using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemy : MonoBehaviour
{
    private StateMachine brain;
    Vector3 dest;

    private NavMeshAgent nav;
    private float idleTimer;

    public float baseSpeed = 3.5f;
    public float chaseMult = 1.5f;

    FieldOfView fov;
    public float DistanceToPlayerBeforeAttacking = 0.6f; 
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        brain = GetComponent<StateMachine>();
        fov = GetComponent<FieldOfView>();
        brain.PushState(ChaseState());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChaseEnter()
    {
        dest = fov.playerRef.transform.position;
        nav.SetDestination(dest);
        nav.autoBraking = true; // Disable auto-braking so the navigation will just keep moving at a steady pace.
        nav.speed = baseSpeed * chaseMult;
    }
    void Chase()
    {
        dest = fov.playerRef.transform.position;
        if (nav.remainingDistance >= DistanceToPlayerBeforeAttacking)
        {
            dest = fov.playerRef.transform.position;
            nav.SetDestination(dest);
        }
        else
        {
            //Debug.Log("ayo the pizza here.");
            dest = fov.playerRef.transform.position;
            nav.SetDestination(dest);
        }
        
    }
    void ChaseExit()
    {
        nav.autoBraking = true;
        nav.speed = baseSpeed;
    }
    State ChaseState()
    {
        return new State(Chase, ChaseEnter, Chase, "Chase");
    }
}
