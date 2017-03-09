using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy2AI3D: EnemyAI3D
{

    public float MoveSpeed = 2.5f;
    public float DashSpeed = 5.0f;
    public float DashRange = 3.0f;
    private NavMeshAgent agent;


    private bool isDead = false;
    private EnemyState enemyState;
    private Rigidbody enemy;
    private float timetoDeath;

    // Update is called once per frame
    void Update()
    {

        var enemyStateData = enemyState.Update(Player.transform.position, Time.deltaTime, agent);
        //calcState(enemyStateData);
        if (enemyStateData.NewState != null)
        {
            ChangeEnemyState(Player.transform.position, enemyStateData);
        }
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
    public float getDashSpeed()
    {
        return DashSpeed;
    }
    public float getDashRange()
    {
        return DashRange;
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




}
