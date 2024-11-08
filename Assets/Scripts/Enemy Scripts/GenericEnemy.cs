using UnityEngine;
using UnityEngine.AI;

public class GenericEnemy : MonoBehaviour
{
	// StateMachine and navigation components, misc external
	public StateMachine brain;
	NavMeshAgent nav;
	NavMeshPath path;
	FieldOfView fov;
	public GameObject hurtBox;
	private Animator wolfAnim;
	[SerializeField]Rigidbody rb;
	// Navigation Parameters
	Vector3 dest; // This originally was meant to be part of a Save system for a longer game loop. May just scrap this variable if no longer needed or keep for debugging purposes.
	[SerializeField] float baseSpeed = 3.5f;
	[SerializeField] float chaseMult = 1.5f, chargeMult = 3f;
	[SerializeField] float wanderDist = 10f;
	[SerializeField] float chargeRange = 5f;
	[SerializeField] float chargeCooldown = 5f, chargeTimer = 0f;
	// Variables for Idling. Rather misleading, the idleTimer gets used for a few more things than just idling, and should be renamed or split into multiple variables.
	public float idleTimer = 3;
	public float currIdle = 0;
	public float ChargeDuration = 1.85f;
	bool hunting = false;
	int backState = 0; // If this number is > 0, it means we need to pop multiple states at once, and this is the safest way to go about this.
	float angleBase; // base viewing angle when navigating.
	Vector3 homePos; // Where the enemy begins. Only wanders within certain range of this point.

	public AudioSource attackSound;

	// Start is called before the first frame update
	// NOTE: Most of these can probably just be assigned in the prefab for the sake of load times on Start.
	// It's not an issue for now, but keep it in mind.
	void Start()
	{
		wolfAnim = GetComponentInChildren<Animator>();
		nav = GetComponent<NavMeshAgent>();
		nav.speed = baseSpeed;
		brain = GetComponent<StateMachine>();
		fov = GetComponent<FieldOfView>();
		brain.PushState(IdleState());
		angleBase = fov.angle;
		hurtBox.SetActive(false);
		path = new(); // Set a blank path just in case some thing needs to check path existence.
		homePos = transform.position;
		// TO DO: Check Maddie's timer for whether at least 50% has passed, then forcibly aggro self.
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
	

	private void Update()
	{
		// Update handles brain override commands such as hunting the player once it sees them, and removing extra states.
		chargeTimer += Time.deltaTime;
		if (fov.canSeePlayer && !hunting)
		{
			nav.CalculatePath(fov.playerRef.transform.position, path);
			if (path.status == NavMeshPathStatus.PathComplete)
			{
				brain.PushState(Chase());
			}
		}
		else if (backState > 0) // This is only called if we need to pop multiple states, such as going from HoldOn -> Chase (normally HoldOn -> Charge -> Chase)
		{
			brain.PopState();
			backState -= 1;
		}
	}

	State IdleState()
	{
		// Initial state of the enemy. Does nothing until conditions are met to either Wander or Chase.
		void IdleEnter()
		{
			nav.ResetPath();
			wolfAnim.SetBool("Running", false);
		}
		void Idle()
		{

			currIdle += Time.deltaTime;
			if (currIdle >= idleTimer)
			{
				brain.PushState(Wander());
				currIdle = Random.Range(0f, idleTimer);
			}

		}
		void IdleExit() // Empty placeholder for State Machine logic.
		{

		}
		return new State(Idle, IdleEnter, IdleExit, "Idle");
	}

	State Wander()
	{
		void WanderEnter()
		{
			Vector3 wanderDir = (wanderDist * Random.insideUnitSphere) + homePos; // Generate a random spot "maxDist" units away from the enemy to navigate towards.
			NavMesh.SamplePosition(wanderDir, out NavMeshHit nvm, wanderDist, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
			AttemptPath(nvm.position);
			dest = nav.destination;
			wolfAnim.SetBool("Running", true);
		}
		void Wander()
		{
			if (nav.remainingDistance <= 0.25f) // Once we're within reasonable distance of the destination, we can leave the state and consider this point "navigated".
			{
				nav.ResetPath();
				brain.PopState();
			}

		}
		void WanderExit()
		{
			wolfAnim.SetBool("Running", false);
		}
		return new(Wander, WanderEnter, WanderExit, "Wander");
	}

	State Chase()
	{
		void Enter()
		{
			chargeTimer = 0f;
			// Toggle the hunting flag, start chasing, and make it so the player has to work harder to evade the enemy than just running past them.
			hunting = true;
			nav.speed = baseSpeed * chaseMult;
			fov.angle = angleBase + 60;
			AttemptPath(fov.playerRef.transform.position);
			wolfAnim.SetBool("Running", true);
		}
		void Update()
		{
			dest = fov.playerRef.transform.position;
			nav.SetDestination(dest);
			//AttemptPath(dest);
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
			wolfAnim.SetBool("Running", false);
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
			NavMesh.SamplePosition(newTarget, out NavMeshHit nvm, chargeRange, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
			nav.SetDestination(nvm.position);
			nav.speed = baseSpeed * chargeMult;
			wolfAnim.SetBool("Running", true);
			chargeTimer = 0f;
		}

		void Update()
		{
			chargeTimer += Time.deltaTime;
			if (nav.remainingDistance < -0.25f || chargeTimer >= ChargeDuration)
			{
				brain.PushState(HoldOn());
			}
		}

		void Exit()
		{
			nav.speed = baseSpeed;
			hurtBox.SetActive(false);
			wolfAnim.SetBool("Running", false);
		}
		return new State(Update, Enter, Exit, "Charge");
	}

	State HoldOn()
	{
		void Enter()
		{
			hurtBox.SetActive(false);
			nav.ResetPath();
			wolfAnim.SetBool("Running", false);
		}
		void Update()
		{
			chargeTimer += Time.deltaTime;
			if (chargeTimer >= ChargeDuration)
			{
				backState++;
				brain.PopState();
			}
		}
		void Exit() // Empty
		{ chargeTimer = 0; }
		return new(Update, Enter, Exit, "HoldUp");
	}

	State GetHit()
    {
		float timer = 0.5f;
		
		void Enter()
        {
			nav.isStopped = true;
			rb.isKinematic = false;
			rb.AddForce(((transform.position - fov.playerRef.transform.position).normalized) * 3, ForceMode.Impulse);
        }

		void Update()
        {
			timer -= Time.deltaTime;
			if (timer <= 0f)
            {
				brain.PopState();
            }
        }

		void Exit()
        {
			nav.isStopped = false;
			rb.isKinematic = true;
        }

		return new(Update, Enter, Exit, "GetHit");
    }
	public void DoDamage()
    {
		brain.PushState(GetHit());
    }
}
