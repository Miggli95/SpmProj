using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    public Animator anim;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("tag" + col.gameObject.tag);
        if (col.CompareTag("player"))  // Movablebox --> implement
        {
            Debug.Log("Is pressed");
            anim.SetBool("IsPressed", true);
            
        }
            
       

    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("is not Pressed");
        if (col.CompareTag("player")) // Movablebox-- > implement
            anim.SetBool("IsPressed", false);
        
    }
	
}
