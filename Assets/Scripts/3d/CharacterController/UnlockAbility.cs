using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    public GameManager manager;
    public int ability;
    public GameObject Player;
    // Use this for initialization


    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other == Player.GetComponent<CapsuleCollider>())
        {
            manager.UnlockAbility(ability);//(int)Abilities.doubleJump);
            Destroy(this.gameObject);
        }
    }
}
