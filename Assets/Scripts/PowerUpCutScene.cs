using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerUpCutScene : MonoBehaviour {

    public GameManager manager;
    
	// Use this for initialization
	void Start () {

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.setRespawn(1);
            SceneManager.LoadScene(8);

        }
    }
    // Update is called once per frame
    void Update () {
        if (manager.HaveAbility((int)Abilities.slam))
        {
            Destroy(this.gameObject);
        }

    }
}
