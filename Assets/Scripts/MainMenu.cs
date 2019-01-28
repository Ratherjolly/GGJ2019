using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space)){
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 3)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 4)
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        //else if(Input.GetKey(KeyCode.E)&& Input.GetKey(KeyCode.X)&& Input.GetKey(KeyCode.I)&& Input.GetKey(KeyCode.T)){
        //    Application.Quit();
        //}
    }
}
