using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenericEnemy : MonoBehaviour
{
    // External Components
    StateMachine brain;
    NavMeshAgent nav;
    NavMeshPath path;
    FieldOfView fov;
    public GameObject hurtBox;

    // Navigation Parameters
    Vector3 dest;
    [SerializeField] float baseSpeed = 3.5f;
    [SerializeField] float chaseMult = 1.5f, chargeMult = 3f;
    [SerializeField] float wanderDist = 10f, attackCooldown = 0.667f;
    [SerializeField] float chargeRange = 5f, attackRange = 2f;
    [SerializeField] float chargeCooldown = 5f, chargeTimer = 0f;
    public float idleTimer = 3;
    public float currIdle = 0;
    bool hunting = false;
    int backState = 0;
    float angleBase;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        nav.speed = baseSpeed;
        brain = GetComponent<StateMachine>();
        fov = GetComponent<FieldOfView>();
        brain.PushState(IdleState());
        angleBase = fov.angle;
        hurtBox.SetActive(false);
        path = new();
    }

    void AttemptPath(Vector3 destination)
    {
        nav.CalculatePath(destination, path);
        if (path.status == NavMeshPathStatus.PathComplete)
        {
            nav.path = path;
        }
        else
        {
            Debug.Log("Oh hey I'm blocked, oh well.");
        }
    }

    private void Update()
    {
        chargeTimer += Time.deltaTime;
        if (fov.canSeePlayer && !hunting)
        {
            nav.CalculatePath(fov.playerRef.transform.position, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                brain.PushState(Chase());
            }
        }
        else if (backState > 0)
        {
            brain.PopState();
            backState -= 1;
        }
    }

    State IdleState()
    {
        void IdleEnter()
        {
            nav.ResetPath();
        }
        void Idle()
        {
            // TODO: Implement checks to enter other states such as Ohio or Florida.

            // Otherwise, wait for IdleTimer to run out, then go somewhere else.
            currIdle += Time.deltaTime;
            if (currIdle >= idleTimer)
            {
                brain.PushState(Wander());
                currIdle = Random.Range(0f, idleTimer);
            }

        }
        void IdleExit() // Empty
        {

        }
        return new State(Idle, IdleEnter, IdleExit, "Idle");
    }

    State Wander()
    {
        void WanderEnter()
        {
            Vector3 wanderDir = (wanderDist * Random.insideUnitSphere) + transform.position; // Generate a random spot "maxDist" units away from the enemy to navigate towards.
            NavMesh.SamplePosition(wanderDir, out NavMeshHit nvm, wanderDist, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
            //nav.SetDestination(nvm.position);
            AttemptPath(nvm.position);
            dest = nav.destination;
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
        return new(Wander, WanderEnter, WanderExit, "Wander");
    }

    State Chase()
    {
        void Enter()
        {
            // Toggle the hunting flag, start chasing, and make it so the player has to work harder to evade the enemy than just running past them.
            hunting = true;
            nav.speed = baseSpeed * chaseMult;
            fov.angle = angleBase + 60;
            //nav.SetDestination(fov.playerRef.transform.position);
            AttemptPath(fov.playerRef.transform.position);
        }
        void Update()
        {


            dest = fov.playerRef.transform.position;
            //nav.SetDestination(dest);
            AttemptPath(dest);
            if ((nav.remainingDistance < chargeRange) && chargeTimer >= chargeCooldown)
            {
                brain.PushState(Charge());
            }
        }
        void Exit()
        {
            nav.speed = baseSpeed;
            fov.angle = angleBase;
            // We aren't sure if we're attacking or Searching, so set hunting to false just in case
            hunting = false;
        }
        return new(Update, Enter, Exit, "Chase");
    }


    State Charge()
    {
        void Enter()
        {
            hurtBox.SetActive(true);
            Vector3 newTarget = fov.playerRef.transform.position - transform.position; // The player destination is exactly where we want to charge. 
            newTarget = chargeRange * 1.2f * newTarget.normalized; // And we want to go in that direction notably farther than just where the player is.
            newTarget += transform.position; // Add this direction + magnitude to our current position, and we should get a proper destination.
            //nav.SetDestination(newTarget);
            AttemptPath(newTarget);
            nav.speed = baseSpeed * chargeMult;
        }

        void Update()
        {
            if (nav.remainingDistance < 0.25f)
            {
                brain.PopState();
            }
        }

        void Exit()
        {
            nav.speed = baseSpeed;
            chargeTimer = 0.0f;
            hurtBox.SetActive(false);
        }
        return new State(Update, Enter, Exit, "Charge");
    }

}
