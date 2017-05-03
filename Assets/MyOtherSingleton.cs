using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyOtherSingleton : MonoBehaviour
{
    public AudioClip scream;
    private static MyOtherSingleton inst = null;
    public static MyOtherSingleton Inst
    {
        get { return inst; }
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

        if (inst != null && inst != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            inst = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        
    }
    public void playVoice()
    {
        GetComponent<AudioSource>().PlayOneShot(scream);
    }

}
