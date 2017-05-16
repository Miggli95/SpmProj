using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyUnitySingleton : MonoBehaviour {
    private static MyUnitySingleton instance = null;
    public static MyUnitySingleton Instance
    {
        get { return instance; }
    }
    void Awake()
    {

    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
      
    }
    void Load()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
   void Update()
    {
        if (SceneManager.GetActiveScene().name != "BossLevel" || SceneManager.GetActiveScene().name != "BossLevel 2" || SceneManager.GetActiveScene().name != "BossLevel 3")
        {
            //play rockin tunes
        }
        else
        {
            Destroy(this.gameObject);
        }
        if(SceneManager.GetActiveScene().name == "EndCredit" || SceneManager.GetActiveScene().name == "Title")
        {
            Destroy(this.gameObject);
        }
    }
}
