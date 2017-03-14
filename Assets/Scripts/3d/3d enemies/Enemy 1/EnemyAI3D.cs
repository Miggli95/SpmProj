using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyAI3D : MonoBehaviour
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


    private bool isDead = false;
    private EnemyState enemyState;
    private Rigidbody enemy;
    private float timetoDeath;

    private AudioSource source1;
    public AudioClip walking;
    private Vector3 lastPosition;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        enemy = GetComponent<Rigidbody>();
        enemyState = GetInitialEnemyState();
        enemyState.Enter();
        source1 = GetComponent<AudioSource>();
        source1.clip = walking;
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
        Debug.Log(distance);
        if(distance > 0.1)
        {
            if (!source1.isPlaying)
            {
                source1.PlayOneShot(walking);
            }
        }
        lastPosition = gameObject.transform.position;
    }

    private void ChangeEnemyState(Vector3 pos, EnemyStateData enemyStateData)
    {
        enemyState.Exit();
        enemyState = enemyStateData.NewState;
        enemyState.Enter();
        if (enemyStateData.RunNewStateSameUpdate)
        {
            enemyState.Update(pos, Time.deltaTime, agent);
        }
    }

   /* private void calcState(EnemyStateData enemyStateData)
    {
        if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= PatrolRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= PatrolRangeY && Mathf.Abs(Player.transform.position.z - transform.position.z) <= PatrolRangeZ)
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= AggroRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeY && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeZ)
            {
                enemyStateData.NewState = new Deal(this);
            }
            else
            {
                enemyState = new Patrol(this);
            }
        else
            currentState = "idle";

    }

    private void idle()
    {
        enemy.velocity = new Vector3(0, 0, 0);
    }
    private void patrol()
    {
        if (points.Length == 0)
            return;
        if (points[destPoint].position.x > transform.position.x)
            enemy.velocity = new Vector3(MoveSpeed, enemy.velocity.y, enemy.velocity.z);
        else
            enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
        if (Mathf.Abs(points[destPoint].position.x - transform.position.x) <= 0.1)
            destPoint = (destPoint + 1) % points.Length;
    }
    private void deal()
    {
        if (Player.transform.position.x > transform.position.x)
            enemy.velocity = new Vector3(MoveSpeed, enemy.velocity.y, enemy.velocity.z);
        else
            enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
    }
    private void dead()
    {
        timetoDeath -= Time.deltaTime;
        if (timetoDeath <= 0)
            Destroy(gameObject);
    }



    public int getDamage()
    {
        return damage;
    }
    public void setSpeed(int newSpeed)
    {
        MoveSpeed = newSpeed;
    }
    public void deathAni()
    {
        enemy.constraints = RigidbodyConstraints.FreezePositionX;

        BoxCollider boxy = GetComponent<BoxCollider>();
        boxy.enabled = false;

        timetoDeath = 3;
        currentState = "dead";
        enemy.velocity = new Vector3(0, -2, 0);
        Debug.Log("Killed enemy");
    } */
    private EnemyState GetInitialEnemyState()
    {
        EnemyState enemyState = null;
        if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= PatrolRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= PatrolRangeY && Mathf.Abs(Player.transform.position.z - transform.position.z) <= PatrolRangeZ)
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= AggroRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeY && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeZ)
            {
                enemyState = new Deal(this);
            }
            else
            {
                enemyState = new Patrol(this);
                GotoNextPoint();
            }
        else
            enemyState= new  Idle(this);
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

    public float GetDashSpeed()
    {
        return 0f;
    }
    public float GetDashRange()
    {
        return 0f;
    }




}
