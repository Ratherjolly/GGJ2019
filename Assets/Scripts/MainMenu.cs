using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;


public class MainMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space)){
            if (EditorSceneManager.GetActiveScene().buildIndex == 0)
                EditorSceneManager.LoadScene(1);
            else if (EditorSceneManager.GetActiveScene().buildIndex == 2)
                EditorSceneManager.LoadScene(0);
            else if (EditorSceneManager.GetActiveScene().buildIndex == 3)
                EditorSceneManager.LoadScene(0);
            else if (EditorSceneManager.GetActiveScene().buildIndex == 4)
                EditorSceneManager.LoadScene(0);
        }
    }
}
