using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyAI2D : MonoBehaviour {
    public GameObject Player;

    public float MoveSpeed = 3.0f;
    public float AggroRangeX=5;
    public float AggroRangeY = 2;
    public int damage;
    private bool isDead = false;
    private Rigidbody enemy;
    void Start () {
        enemy = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead== false)
        {
            bool gotcommand = false;
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) <= AggroRangeX && Mathf.Abs(Player.transform.position.y - transform.position.y) <= AggroRangeY)
            {
                if (Player.transform.position.x > transform.position.x)
                    enemy.velocity = new Vector3(MoveSpeed, enemy.velocity.y, enemy.velocity.z);
                else
                    enemy.velocity = new Vector3(-MoveSpeed, enemy.velocity.y, enemy.velocity.z);
                gotcommand = true;
            }
            if (gotcommand == false)
                enemy.velocity = new Vector3(0, 0, 0);
        }
    }
    public int getDamage()
    {
        return damage;
    }
    public void deathAni()
    {
        enemy.constraints = RigidbodyConstraints.FreezePositionX;
        
        BoxCollider boxy = GetComponent<BoxCollider>();
        boxy.enabled= false;
        isDead = true;
        enemy.velocity = new Vector3(0, -2, 0);
        Debug.Log("Killed enemy");
    }
}
