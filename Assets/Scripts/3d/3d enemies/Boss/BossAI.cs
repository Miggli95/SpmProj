using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject Player;

    public bool isIncapacitated = false;
    public float MoveSpeed = 2.5f;
    public float DashSpeed = 5.0f;
    public float DashRange = 3.0f;
    public float PatrolRangeX = 10;
    public float PatrolRangeY = 10;
    public float PatrolRangeZ = 10;
    public float AggroRangeX = 5;
    public float AggroRangeY = 2;
    public float AggroRangeZ = 5;
    public int damage;
    private Vector3 lastAgentVelocity;
    private NavMeshPath lastAgentPath;
    protected NavMeshAgent agent;
    public Transform[] points;
    public int destPoint = 0;
    private bool isDead = false;
    private BossState enemyState;
    private Rigidbody enemy;
    private float timetoDeath;

   /* private AudioSource[] sources;
    public AudioClip walking;
    public AudioClip dash;
    public AudioClip electricStatic;
    public AudioClip dashCharge; */
    private Vector3 lastPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        enemy = GetComponent<Rigidbody>();
        enemyState = GetInitialEnemyState();
        enemyState.Enter();
       // sources = GetComponents<AudioSource>();
        //sources[0].clip = walking;
        //sources[1].clip = electricStatic;
    }

    // Update is called once per frame
    void Update()
    {

        var enemyStateData = enemyState.Update(Player.transform.position, Time.deltaTime, agent);
        //calcState(enemyStateData);
        if (enemyStateData.NewState != null)
        {
            ChangeEnemyState(Player.transform.position, enemyStateData);
        }

        float distance = Vector3.Distance(lastPosition, gameObject.transform.position);
        /*if (distance > 0.05)
        {
            if (!sources[0].isPlaying)
            {
                sources[0].PlayOneShot(walking);
            }
        }*/
        lastPosition = gameObject.transform.position;
        /*if (!sources[1].isPlaying)
        {
            sources[1].PlayDelayed(0.5f);
        }*/
    }

    private void ChangeEnemyState(Vector3 pos, BossStateData enemyStateData)
    {
        enemyState.Exit();
        enemyState = enemyStateData.NewState;
        enemyState.Enter();
        if (enemyStateData.RunNewStateSameUpdate)
        {
            enemyState.Update(pos, Time.deltaTime, agent);
        }
    }
    private BossState GetInitialEnemyState()
    {
        BossState enemyState = null;
                enemyState = new BossDeal(this);
        return enemyState;
    }
    public void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
    public void Pause()
    {
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }
    public void Resume()
    {
        agent.velocity = lastAgentVelocity;
        agent.SetPath(lastAgentPath);
    }
    public void StartChase()
    {
        lastAgentPath = agent.path;
        agent.ResetPath();
    }
    public void EndChase()
    {
        agent.SetPath(lastAgentPath);
    }
    public float getDashSpeed()
    {
        return DashSpeed;
    }
    public float getDashRange()
    {
        return DashRange;
    }



    public int getDamage()
    {
        Vector3 spawn = new Vector3(6, 14, 2);

        Player.GetComponent<CharController>().Death();

        return damage;
    }
    public void setSpeed(int newSpeed)
    {
        MoveSpeed = newSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Spawner>().fillHole();
       // Destroy(gameObject);
        
    }
    public void deathAni()
    {
        //nah
    }


}
