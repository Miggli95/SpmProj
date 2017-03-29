using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAI2D : MonoBehaviour {
    public GameObject Player;

    public float MoveSpeed = 3.0f;
    public float PatrolRangeX = 10;
    public float PatrolRangeY = 10;
    public float AggroRangeX=5;
    public float AggroRangeY = 2;
    public int damage;

    public Transform[] points;
    private int destPoint = 0;


    private bool isDead = false;
    private string currentState;
    private Rigidbody enemy;
    private float timetoDeath;

    private AudioSource[] sources;
    public AudioClip patrolSound;
    public AudioClip[] dealDamageSound;
    private int clipIndex;

    void Start () {
        enemy = GetComponent<Rigidbody>();
        currentState = "idle";
        sources = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
        calcState();
        switch (currentState) {
            case "idle":
                idle();
                break;
            case "patrol":
                patrol();
                break;
            case "deal":
                deal();
                break; 
            case "dead":
                dead();
                break;
        }
    }

    private void calcState()
    {

        if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= PatrolRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= PatrolRangeY)
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= AggroRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeY)
            {
                setSpeed(2);
                currentState = "deal";
            }
            else
            {
                setSpeed(1);
                currentState = "patrol";
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
        {
            enemy.velocity = new Vector3(MoveSpeed, enemy.velocity.y, enemy.velocity.z);
            sources[0].clip = patrolSound;
            if (!sources[0].isPlaying)
                sources[0].PlayOneShot(patrolSound);
        }
        else
        {
            enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
            sources[0].clip = patrolSound;
            if (!sources[0].isPlaying)
                sources[0].PlayOneShot(patrolSound);
        }
        if(Mathf.Abs(points[destPoint].position.x - transform.position.x) <= 0.1)
            destPoint = (destPoint + 1) % points.Length;
    }
    private void deal()
    {
        if (Player.transform.position.x > transform.position.x)
        {
            enemy.velocity = new Vector3(MoveSpeed, enemy.velocity.y, enemy.velocity.z);
            sources[0].clip = patrolSound;
            if (!sources[0].isPlaying)
                sources[0].PlayOneShot(patrolSound);
        }
        else
        {
            enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
            sources[0].clip = patrolSound;
            if (!sources[0].isPlaying)
                sources[0].PlayOneShot(patrolSound);
        }
    }
    private void dead()
    {
        timetoDeath -= Time.deltaTime;
        if (timetoDeath <= 0)
            Destroy(gameObject);
    }



    public int getDamage()
    {
        Vector3 spawn;
        if (transform.position.y > 0.0f)
        {
            spawn = new Vector3(-3, 2, 0);
            Player.GetComponent<player2d_controller>().Die(spawn);
        }
        else
        {
            spawn = new Vector3(5, -11, 0);
            Player.GetComponent<player2d_controller>().Die(spawn);
        }
        return damage;
    }
    public void setSpeed(int newSpeed)
    {
        MoveSpeed = newSpeed;
    }
    public void deathAni()
    {
        /*clipIndex = Random.Range(0, dealDamageSound.Length);
        if (!sources[1].isPlaying)
            sources[1].PlayOneShot(dealDamageSound[clipIndex]);
            */
        enemy.constraints = RigidbodyConstraints.FreezePositionX;
        
        BoxCollider boxy = GetComponent<BoxCollider>();
        boxy.enabled= false;

        timetoDeath = 3;
        currentState = "dead";
        enemy.velocity = new Vector3(0, -2, 0);
        Debug.Log("Killed enemy");
    }        
}
