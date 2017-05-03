using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAI2D : MonoBehaviour {
    public GameObject Player;

    public GameObject deathP;
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
    public AudioClip heartBeat;
    private int clipIndex;

    void Start () {
        enemy = GetComponent<Rigidbody>();
        currentState = "idle";
        sources = GetComponents<AudioSource>();
        sources[2].clip = heartBeat;
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

       /* if (!sources[2].isPlaying)
        {
            sources[2].PlayDelayed(0.5f);
        }*/
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
            {
                sources[0].pitch = 1;
                sources[0].PlayOneShot(patrolSound);
            }
        }
        else
        {
            enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
            sources[0].clip = patrolSound;
            if (!sources[0].isPlaying)
            {
                sources[0].pitch = 1;
                sources[0].PlayOneShot(patrolSound);
            }
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
            {
                sources[0].pitch = 1.5f;
                sources[0].PlayOneShot(patrolSound);
            }
        }
        else
        {
            enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
            sources[0].clip = patrolSound;
            if (!sources[0].isPlaying)
            {
                sources[0].pitch = 1.5f;
                sources[0].PlayOneShot(patrolSound);
            }
        }
        RaycastHit rayhit;
        if (Physics.Raycast(transform.position, Vector3.left, out rayhit))
        {
            if (rayhit.collider.tag == "player" && rayhit.distance < 0.6f && rayhit.collider is CapsuleCollider)
                getDamage();
        }
        if (Physics.Raycast(transform.position, Vector3.right, out rayhit))
        {
            if(rayhit.collider.tag == "player" && rayhit.distance < 0.6f && rayhit.collider is CapsuleCollider)
                getDamage();
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
        Player.GetComponent<CharController2D>().Respwn();
        return damage;
    }
    public void setSpeed(int newSpeed)
    {
        MoveSpeed = newSpeed;
    }
    public void deathAni()
    {
        Instantiate(deathP, transform.position, Quaternion.identity);
        enemy.constraints = RigidbodyConstraints.FreezePositionX;
        
        BoxCollider boxy = GetComponent<BoxCollider>();
        boxy.enabled= false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject.transform.GetChild(0).gameObject);
        timetoDeath = 7;
        currentState = "dead";
        enemy.velocity = new Vector3(0, -2, 0);
        Debug.Log("Killed enemy");

        clipIndex = Random.Range(0, dealDamageSound.Length);
        if (!sources[1].isPlaying)
            sources[1].PlayOneShot(dealDamageSound[clipIndex]);
     
    }
}
