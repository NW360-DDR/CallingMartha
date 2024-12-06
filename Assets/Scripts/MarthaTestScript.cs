using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class MarthaTestScript : MonoBehaviour
{
	// StateMachine and navigation components, misc external
	public StateMachine brain;
	NavMeshAgent nav;
	NavMeshPath path;
	FieldOfView fov;
	Animator anim;
	public GameObject hurtBox;
	public GameObject killBox;
	Enemy health;
	public EclipseTimer gameTimer;

	// Navigation Parameters
	public Vector3 dest;
	readonly float maxWanderDist = 20f;
	public float idleTimer = 3f;
	float currIdle = 0f;
	readonly float baseSpeed = 20f;
	readonly float chaseMult = 1.5f;
	bool hunting = false;
	GameObject warpObject;

	int backState = 0; // If this number is > 0, it means we need to pop multiple states at once, and this is the safest way to go about this.

	//Boss Fight Variables
	public float attackCooldown = 0.75f;
	readonly int BossHealth = 500;
	readonly float bossSpeed = 25f;
	readonly float chargeRange = 5f;
	readonly float chargeCooldown = 5f;
	float chargeTimer = 0f;
	float deNavTimer = 0f; // How long until the boss gives up and walks away.
	readonly float RetreatTimer = 3f;

	// Start is called before the first frame update
	void Start()
	{
		nav = gameObject.GetComponent<NavMeshAgent>();
		fov = gameObject.GetComponent<FieldOfView>();
		anim = gameObject.GetComponentInChildren<Animator>();

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
		hunting = true;
		if (!brain.gameObject.activeSelf)
		{
			brain.gameObject.SetActive(true); // Make sure the brain is active. Probably no longer needed.
		}

		if (gameTimer.enteredArena) // If we've entered the arena, warpObject goes to the arena and we Start the boss fight.
		{
			warpObject = GameObject.Find("ArenaWarp");
			StartBossFight();
			nav.Warp(warpObject.transform.position);
		}
		else
		{
			brain.PushState(MurderHobo());
		}
		
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
	State MurderHobo()
	{
		void Enter()
		{
			Destroy(hurtBox);
			dest = fov.playerRef.transform.position;
			nav.SetDestination(dest);
			nav.speed = baseSpeed * chaseMult * 2; // Eviscerate this man's spinal column
			killBox.SetActive(true);
			nav.radius = 0.01f;
			nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
			nav.autoBraking = false;
			fov.canSeePlayer = true;
			anim.SetBool("Running", true);
		}
		void Update()
		{
			dest = fov.playerRef.transform.position;
			AttemptPath(dest);

			if (nav.remainingDistance < 3)
			{
				killBox.SetActive(true);
                anim.SetBool("Attacking", true);
            }else
			{
                killBox.SetActive(true);
                anim.SetBool("Attacking", false);

            }

            //health.health = 42069;
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
		void Enter()
		{
            anim.SetBool("Running", true);
        }
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
			anim.SetBool("Attacking", true);
		}

		void Update()
		{
			if (hurtBox != null) hurtBox.SetActive(true);
			if (nav.remainingDistance < 1f)
			{
				chargeTimer = 0.0f;
				brain.PushState(HoldOn());
			}
		}

		void Exit()
		{
			nav.speed = baseSpeed;
			hurtBox.SetActive(false);
            anim.SetBool("Attacking", false);
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
	State HoldOn()
	{
		void Enter()
		{
			hurtBox.SetActive(false);
			nav.ResetPath();
			currIdle = 1;
            anim.SetBool("Running", false);
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
	
	public void DoDamage()
	{
		brain.PushState(HoldOn());
	}
}
