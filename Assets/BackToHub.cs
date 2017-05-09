using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToHub : MonoBehaviour
{
    public void LoadHub()
    {
        SceneManager.LoadScene("Title");
    }
}
