using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    public Animator anim;
    

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("tag" + col.gameObject.tag);
<<<<<<< HEAD
        if (col.CompareTag("player") || col.CompareTag("movableBox"))  // Movablebox --> implement
=======
        if (col.CompareTag("player"))  // Movablebox --> implement
>>>>>>> parent of 948922f... sounds to animation and movable box trigger
        {
            Debug.Log("Is pressed");
            anim.SetBool("IsPressed", true);
            // ((DoorSlide)GameObject.FindGameObjectWithTag("Door") ).openDoor();
            var d = GameObject.Find("Door").gameObject.GetComponent<DoorSlide>();
            d.openDoor();
        }
        


    }

    void OnTriggerExit(Collider col)
    {
        GameObject.Find("Door").gameObject.GetComponent<DoorSlide>().closeDoor();
        Debug.Log("is not Pressed");
        if (col.CompareTag("player")) // Movablebox-- > implement
            anim.SetBool("IsPressed", false);
        
    }
	
}
