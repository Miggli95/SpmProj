using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    public GameManager manager;
    public int ability;
    // Use this for initialization


    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        manager.UnlockAbility(ability);//(int)Abilities.doubleJump);
        Destroy(this.gameObject);
    }
}
