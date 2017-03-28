using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy3AI3D : MonoBehaviour
{
    public GameObject Player;

    public float MoveSpeed = 3.0f;
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
    private int health = 2;

    private bool isDead = false;
    private EnemyState3 enemyState;
    private Rigidbody enemy;
    private float timetoDeath;

    //private AudioSource[] sources;
   // public AudioClip walking;
    //public AudioClip sonarPing;
    private Vector3 lastPosition;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        enemy = GetComponent<Rigidbody>();
        enemyState = GetInitialEnemyState();
        enemyState.Enter();
       // sources = GetComponents<AudioSource>();
       // sources[0].clip = walking;
       // sources[1].clip = sonarPing;
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
        //Debug.Log(distance);
       /* if (distance > 0.1)
        {
            if (!sources[0].isPlaying)
            {
                sources[0].PlayOneShot(walking);
            }
        }
        lastPosition = gameObject.transform.position;

        if (!sources[1].isPlaying)
        {
            sources[1].PlayDelayed(2);
        }*/
    }

    private void ChangeEnemyState(Vector3 pos, Enemy3StateData enemyStateData)
    {
        enemyState.Exit();
        enemyState = enemyStateData.NewState;
        enemyState.Enter();
        if (enemyStateData.RunNewStateSameUpdate)
        {
            enemyState.Update(pos, Time.deltaTime, agent);
        }
    }

    public int getDamage()
    {
        Vector3 spawn = new Vector3(6, 14, 2);

        Player.GetComponent<CharController>().Death(spawn);

        return damage;
    }

    private EnemyState3 GetInitialEnemyState()
    {
        EnemyState3 enemyState = null;
        if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= PatrolRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= PatrolRangeY && Mathf.Abs(Player.transform.position.z - transform.position.z) <= PatrolRangeZ)
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= AggroRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeY && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeZ)
            {
                enemyState = new Enemy3Deal(this);
            }
            else
            {
                enemyState = new Enemy3Patrol(this);
                GotoNextPoint();
            }
        else
            enemyState = new Enemy3Idle(this);
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

    public void deathAni()
    {
        takeDamage();
    }
    private void takeDamage()
    {
        health--;
        if(health == 1)
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
        if(health == 0)
        {
            kill();
        }
    }

    public void kill()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == Player.GetComponent<CapsuleCollider>())
        {
            print(transform.position.y - Player.transform.position.y);
            if (transform.position.y - Player.transform.position.y <= -1.2f)
            {

                //Player.GetComponent<CharController>().forceJump();
                //kill();
            }
            else
            {
                getDamage();
            }
        }
    }

}
