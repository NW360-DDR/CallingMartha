using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MarthaTestScript : MonoBehaviour
{
	// StateMachine and navigation components, misc external
	public StateMachine brain;
	[SerializeField] NavMeshAgent nav;
	NavMeshPath path;
	[SerializeField] FieldOfView fov;
	[SerializeField] Animator anim;
	public GameObject hurtBox;
	Enemy health;

	// Navigation Parameters
	public Vector3 dest;
	public Vector3 lastKnownPlayerLoc;
	readonly float maxWanderDist = 20f;
	public float idleTimer = 3f;
	float currIdle = 0f;
	public float baseSpeed = 3.5f;
	public float chaseMult = 1.5f;

	bool hunting = false;

	int backState = 0; // If this number is > 0, it means we need to pop multiple states at once, and this is the safest way to go about this.
	float angleBase; // base viewing angle when navigating.
	
	// Start is called before the first frame update
	// Unlike the Generic enemy, these are serialized and therefore shouldn't need a GetComponent call.
	void Start()
	{
		brain.PushState(Idle());
		idleTimer = Mathf.Infinity;
		path = new();
		angleBase = fov.angle;
	}

	private void Update()
	{
		if (fov.canSeePlayer && !hunting)
		{
			hunting = true;
			brain.PushState(ChaseState());
		}
		if (backState > 0)
        {
			brain.PopState();
			backState--;
        }
	}

	public void KILL()
    {
		brain.PushState(MurderHobo());
		Destroy(health);
    }

	/// <summary>
	/// Checks if the path in question can be reached.
	/// </summary>
	/// <param name="destination"></param>
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

	State Wander()
	{
		void WanderEnter()
		{
			Vector3 wanderDir = (UnityEngine.Random.insideUnitSphere * maxWanderDist) + transform.position; // Generate a random spot "maxWanderDist" units away from the enemy to navigate towards.
			NavMesh.SamplePosition(wanderDir, out NavMeshHit nvm, maxWanderDist, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
			nav.SetDestination(nvm.position);
			dest = nav.destination;
		}
		void Wander()
		{
			if (nav.remainingDistance <= 0.25f) // Hey are we close to the destination?
			{
				nav.ResetPath();
				brain.PushState(Idle());
			}

		}
		void WanderExit()
		{
			// Empty
		}
		return new State(Wander, WanderEnter, WanderExit, "Wander");
	}
	State Look()
	{
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
					brain.PushState(Look());
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
	State Idle()
	{
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
		return new State(Idle, IdleEnter, IdleExit, "Idle");
	}
	State MurderHobo()
    {
		void Enter()
		{
			dest = fov.playerRef.transform.position;
			nav.SetDestination(dest);
			nav.autoBraking = false; // Disable auto-braking so the navigation will just keep moving at a steady pace.
			nav.speed = baseSpeed * chaseMult * 10; // Eviscerate this man's spinal column
		}
		void Update()
        {
			dest = fov.playerRef.transform.position;
			AttemptPath(dest);
			if (nav.remainingDistance < 0.5f)
            {
				hurtBox.SetActive(true);
            }
        }
		
		void Exit()
		{
			// There is no exiting MurderHobo mode.
			brain.PushState(MurderHobo());
			// There is only MurderHobo.
		}


		return new(Update, Enter, Exit, "MurderHobo");
    }
}