using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MarthaTestScript : MonoBehaviour
{
    public StateMachine brain;
    public Vector3 dest;
    public Vector3 lastKnownPlayerLoc;

    private NavMeshAgent nav;
    private float idleTimer;

    public float baseSpeed = 3.5f;
    public float chaseMult = 1.5f;

    // logic for state changes
    FieldOfView fov;
    public string Currstate;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        brain = GetComponent<StateMachine>();
        fov = GetComponent<FieldOfView>();
        brain.PushState(IdleState());
    }

    private void Update()
    {
        if (fov.canSeePlayer && brain.GetState() != "Chase")
        {
            brain.PushState(ChaseState());
        }
        Currstate = brain.GetState();
    }

    
    
    
   
    State Wander()
    {
        void WanderEnter()
        {
            float maxDist = 10f;
            Vector3 wanderDir = (UnityEngine.Random.insideUnitSphere * maxDist) + transform.position; // Generate a random spot "maxDist" units away from the enemy to navigate towards.
            NavMesh.SamplePosition(wanderDir, out NavMeshHit nvm, maxDist, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
            nav.SetDestination(nvm.position);
            dest = nav.destination;
        }
        void Wander()
        {
            if (nav.remainingDistance <= 0.25f) // Hey are we close to the destination?
            {
                nav.ResetPath();
                brain.PushState(IdleState());
            }

        }
        void WanderExit()
        {
            // Empty
        }
        return new State(Wander, WanderEnter, WanderExit, "Wander");
    }
    State LookState()
    {
        name = "LookAround";
        void LookAroundEnter()
        {
            idleTimer = 8f;

        }// TODO: Use this state to look for the player once we lose them for "idleTimer" seconds. Whatever that ends up being. Basically an angry Wander state with a few lookAround additions.
        void LookAround()
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                brain.PushState(Wander());
            }
            else if (nav.remainingDistance <= 0.25)// If we still have time to look, and we aren't moving to look, pick a random spot around where we last saw the player to start looking again.
            {
                Vector3 searchDir = (UnityEngine.Random.insideUnitSphere * 5f) + lastKnownPlayerLoc;
                NavMesh.SamplePosition(searchDir, out NavMeshHit nvm, 5f, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
                nav.SetDestination(nvm.position);
                dest = nav.destination;
            }
        }
        void LookAroundExit()
        {
            idleTimer = 5f;
        }
        return new State(LookAround, LookAroundEnter, LookAroundExit, "LookAround");
    }
    State ChaseState() 
    {
        name = "Chase";
        void ChaseEnter()
        {
            dest = fov.playerRef.transform.position;
            nav.SetDestination(dest);
            nav.autoBraking = false; // Disable auto-braking so the navigation will just keep moving at a steady pace.
            nav.speed = baseSpeed * chaseMult;
        }
        void Chase()
        {

            dest = fov.playerRef.transform.position;
            lastKnownPlayerLoc = dest;
            if (fov.canSeePlayer) //If we can still see the player, keep going.
            {
                nav.SetDestination(dest);
            }
            else // If not, keep moving to the destination, and when we get there, scream into the void
            {
                if (nav.remainingDistance <= 0.25f) // Hey are we close to the destination?
                {
                    nav.ResetPath();
                    brain.PushState(LookState());
                }

            }
        }
        void ChaseExit()
        {
            nav.autoBraking = true;
            nav.speed = baseSpeed;
        }
        return new State(Chase, ChaseEnter, ChaseExit, "Chase");
    }
    State IdleState()
    {
        name = "Idle";
        void IdleEnter()
        {
            nav.ResetPath();
        }
        void Idle()
        {
            // TODO: Implement checks to enter other states such as Ohio or Florida.

            // Otherwise, wait for IdleTimer to run out, then go somewhere else.
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                brain.PushState(Wander());
                idleTimer = UnityEngine.Random.Range(1, 3);
            }

        }
        void IdleExit() // Empty
        {

        }
        return new State(Idle, IdleEnter, IdleExit, name);
    }
}