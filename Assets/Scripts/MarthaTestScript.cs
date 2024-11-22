using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.SceneManagement;

public class MarthaTestScript : MonoBehaviour
{
	// StateMachine and navigation components, misc external
	public StateMachine brain;
	[SerializeField] NavMeshAgent nav;
	NavMeshPath path;
	[SerializeField] FieldOfView fov;
	[SerializeField] Animator anim;
	public GameObject hurtBox;
	public GameObject killBox;
	Enemy health;
	public EclipseTimer gameTimer;

	// Navigation Parameters
	public Vector3 dest;
	public Vector3 lastKnownPlayerLoc;
	readonly float maxWanderDist = 20f;
	public float idleTimer = 3f;
	float currIdle = 0f;
	public float baseSpeed = 25f;
	public float chaseMult = 1.5f;
	bool hunting = false;

	int backState = 0; // If this number is > 0, it means we need to pop multiple states at once, and this is the safest way to go about this.

	//Boss Fight Variables
	public float attackCooldown = 0.75f;
	[SerializeField] int BossHealth = 500;
	public float bossSpeed = 25f;
	[SerializeField] float chargeRange = 5f;
	[SerializeField] float chargeCooldown = 5f, chargeTimer = 0f;
	float deNavTimer = 0f; // How long until the boss gives up and walks away.
	public float RetreatTimer = 3f;

	// Start is called before the first frame update
	void Start()
	{
		brain.PushState(DoNothingUntilCalled());
		path = new();
	}

	void Update()
	{
		chargeTimer += Time.deltaTime;
		if (fov.canSeePlayer && !hunting)
		{
			hunting = true;
			brain.PushState(BossFight_Chase());
		}
		else
		{
			idleTimer = 3f;
		}
		if (backState > 0)
		{
			brain.PopState();
			backState--;
			chargeTimer++;
		}
		if (health != null)
		{
			if (health.health <= 0f)
			{
				SceneManager.LoadScene("End Scene");
			}
		}
	}
	
	public void KILL()
	{
		if (!brain.gameObject.activeSelf)
		{
			brain.gameObject.SetActive(true);
		}
		/*
		// It seems we want to make Martha start off closer than she usually is. I personally disagree, but fair enough.
		Vector3 temp = fov.playerRef.transform.position;
		float rand = UnityEngine.Random.Range(0, 360);
		temp += new Vector3(MathF.Cos(rand), 100, MathF.Sin(rand) * (fov.radius * 0.3f));
		Physics.Raycast(temp, Vector3.down, out RaycastHit checkVert, Mathf.Infinity);
		temp.y = checkVert.transform.position.y;
		transform.position = temp;
		*/
		brain.PushState(MurderHobo());
		Destroy(health);
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
	void AttemptPathBoss(Vector3 destination)
	{
		nav.CalculatePath(destination, path);
		if (path.status == NavMeshPathStatus.PathComplete)
		{
			nav.path = path;

		}
		else // This will only occur if the wolf physically cannot make it to the player.
		{
			deNavTimer += Time.deltaTime;
		}
	}
	public void StartBossFight()
	{
		brain.PushState(BossFight_Chase());
		nav.speed = bossSpeed;
		idleTimer = 3f;
		health = GetComponent<Enemy>();
		if (health != null)
		{
			health.health = BossHealth;
			AttemptPathBoss(fov.playerRef.transform.position);
		}
	}
	
	State DoNothingUntilCalled()
	{
		static void yee(){}

		return new(yee, yee, yee, "Awaiting State...");
	}
	State HoldOn()
	{
		void Enter()
		{
			hurtBox.SetActive(false);
			nav.ResetPath();
			currIdle = 1;
			//wolfAnim.SetBool("Running", false);
		}
		void Update()
		{
			// Otherwise, wait for IdleTimer to run out, then go somewhere else.
			currIdle += Time.deltaTime;
			if (currIdle >= idleTimer)
			{
				backState++;
				brain.PopState();
			}
		}
		void Exit() { }
		return new(Update, Enter, Exit, "HoldUp");
	}
	State MurderHobo()
	{
		void Enter()
		{
			dest = fov.playerRef.transform.position;
			nav.SetDestination(dest);
			nav.speed = baseSpeed * chaseMult * 10; // Eviscerate this man's spinal column
			killBox.SetActive(true);
		}
		void Update()
		{
			dest = fov.playerRef.transform.position;
			nav.SetDestination(dest);
			killBox.SetActive(true);
		}
		
		void Exit()
		{
			// There is no exiting MurderHobo mode.
			brain.PushState(MurderHobo());
			// There is only MurderHobo.
		}


		return new(Update, Enter, Exit, "MurderHobo");
	}
	State BossFight_Chase()
	{
		void Enter(){}
		void Update()
		{
			AttemptPathBoss(fov.playerRef.transform.position);
			if (deNavTimer >= RetreatTimer) // Implying we haven't been able to reach the player for a bit.
			{
				brain.PushState(BossBackAway());
			}
			if ((nav.remainingDistance < chargeRange) && chargeTimer >= chargeCooldown)
			{
				brain.PushState(Attack());
			}
		}
		void Exit()
		{
			deNavTimer = 0;
		}
		return new(Update, Enter, Exit, "Boss Chase");
	}
	State Attack()
	{
		float chargeMult = 3f;
		void Enter()
		{
			chargeTimer = 0.0f;
			hurtBox.SetActive(true);
			Vector3 newTarget = fov.playerRef.transform.position - transform.position; // The player destination is exactly where we want to charge. 
			newTarget = chargeRange * 1.2f * newTarget.normalized; // And we want to go in that direction notably farther than just where the player is.
			newTarget += transform.position; // Add this direction + magnitude to our current position, and we should get a proper destination.
			AttemptPath(newTarget);
			nav.speed = baseSpeed * chargeMult;
			//wolfAnim.SetBool("Running", true);
		}

		void Update()
		{
			hurtBox.SetActive(true);
			if (nav.remainingDistance < 0.25f)
			{
				chargeTimer = 0.0f;
				brain.PushState(HoldOn());
			}
		}

		void Exit()
		{
			nav.speed = baseSpeed;
			hurtBox.SetActive(false);
			//wolfAnim.SetBool("Running", false);
		}
		return new State(Update, Enter, Exit, "Charge");
	}
	State BossBackAway()
	{
		void WanderEnter()
		{
			while (nav.pathStatus != NavMeshPathStatus.PathComplete)
			{
				Vector3 wanderDir = (UnityEngine.Random.insideUnitSphere * maxWanderDist) + transform.position; // Generate a random spot "maxWanderDist" units away from the enemy to navigate towards.
				NavMesh.SamplePosition(wanderDir, out NavMeshHit nvm, maxWanderDist, NavMesh.AllAreas); // God I hope this is a spot that can be navigated to.
				AttemptPath(nvm.position);
			}
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
		void WanderExit(){}
		return new State(Wander, WanderEnter, WanderExit, "Backing Away");
	}

}