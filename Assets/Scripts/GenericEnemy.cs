using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemy : MonoBehaviour
{
    private StateMachine brain;
    private Vector3 Dest { get; set;}
    Vector3 lastKnownPlayerLoc;
    private NavMeshAgent nav;
    private float idleTimer;

    public float baseSpeed = 3.5f;
    public float chaseMult = 1.5f;
    public float chargeMult = 2.1f;

    string currState;

    FieldOfView fov;
    public float DistanceToCharge = 0.6f;

    float idleTimer;
    public float wanderTime;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        brain = GetComponent<StateMachine>();
        fov = GetComponent<FieldOfView>();
        brain.PushState(Idle());
    }

    void Update()
    {
        currState = brain.GetState();
        // According to Unity documentation, doing string comparison this way is an astronomical time saver 
        if (fov.canSeePlayer && !((Matches(currState, "Chase") || Matches(currState, "Charge"))))
        {
            brain.PushState(Chase());
        }
    }

    public static bool Matches(string a, string b)
    {
        int ap = a.Length - 1;
        int bp = b.Length - 1;

        while (ap >= 0 && bp >= 0 && a[ap] == b[bp])
        {
            ap--;
            bp--;
        }

        return (bp < 0);
    }


    State Charge()
    {
        void ChargeEnter()
        {
            
            nav.speed = baseSpeed * chargeMult;
            Dest = fov.playerRef.transform.position - transform.position.normalized;
            Dest *= 1.2f;
            nav.SetDestination(Dest);
        }
        void Charge()
        {
            if (nav.remainingDistance <= 0.25f) // Hey are we close to the destination?
            {
                nav.ResetPath();
                brain.PushState(Look());
            }
        }
        void ChargeExit()
        {
            Debug.Log("Exiting Charge.");
            nav.speed = baseSpeed * chaseMult;
            // Now that we're getting out of this state, is the player still within radius? If not, nuke it
            if (!fov.WithinRadius())
            {
                Debug.Log("Exiting Charge, but with cool glasses.");
            }
        }
        return new (ChargeEnter, Charge, ChargeExit, "Charge");
    }

    void Blank()
    {
        Debug.Log("Exiting current State.");
    }

    State Chase()
    {
        void ChaseEnter()
        {
            Dest = fov.playerRef.transform.position;
            nav.SetDestination(Dest);
            nav.autoBraking = true; // Disable auto-braking so the navigation will just keep moving at a steady pace.
            nav.speed = baseSpeed * chaseMult;
        }
        void Chase()
        {
            Dest = fov.playerRef.transform.position;
            lastKnownPlayerLoc = Dest;
            if (fov.canSeePlayer) //If we can still see the player, keep going.
            {
                if (nav.remainingDistance > DistanceToCharge)
                {
                    nav.SetDestination(Dest);
                }
                else
                {
                    brain.PushState(Charge());
                }
                
            }
            else // If not, keep moving to the destination, and when we get there, scream into the void
            {
                if (nav.remainingDistance <= 0.25f) // Hey are we close to the destination?
                {
                    nav.ResetPath();
                    brain.PushState(Look());
                }
                else if (nav.path == null)
                {
                    brain.PushState(Idle());
                }

            }
        }
        return new(Chase, ChaseEnter, Blank, "Chase");
    }

    State Idle()
    {
        void IdleEnter()
        {
            nav.ResetPath();
            SetIdleTime(UnityEngine.Random.Range(1, wanderTime));
        }
        void Idle()
        {
            // TODO: Implement checks to enter other states such as Ohio or Florida.

            // Otherwise, wait for IdleTimer to run out, then go somewhere else.
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                brain.PushState(Wander());

            }

        }
        void IdleExit() // Empty
        {
            SetIdleTime(UnityEngine.Random.Range(1, 3));
        }
        return new State(Idle, IdleEnter, IdleExit, "Idle");
    }

    public State Wander()
    {
        void WanderEnter()
        {
            float maxDist = 10f;
            Vector3 wanderDir = (UnityEngine.Random.insideUnitSphere * maxDist) + transform.position; // Generate a random spot "maxDist" units away from the enemy to navigate towards.
            NavMesh.SamplePosition(wanderDir, out NavMeshHit nvm, maxDist, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
            nav.SetDestination(nvm.position);
            Dest = nav.destination;
        }
        void Wander()
        {
            if (nav.remainingDistance <= 0.25f) // Hey are we close to the destination?
            {
                nav.ResetPath();
                brain.PopState();
            }

        }
        void WanderExit()
        {
            // Empty
        }
        return new State(Wander, WanderEnter, WanderExit, "Wander");
    }

    public State Look()
    {
        name = "Look";
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
                Dest = nav.destination;
            }
        }
        void LookAroundExit()
        {
            SetIdleTime(wanderTime);
            brain.PushState(Idle());
        }
        return new State(LookAround, LookAroundEnter, LookAroundExit, "LookAround");
    }

    public void SetIdleTime(float amt)
    {
        idleTimer = amt;
    }
}
