using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reLoadScene : MonoBehaviour
{
    void OnGUI()
    {

        if (GUI.Button(new Rect(100, 100, 100, 100), "Click"))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
            
    }
}
