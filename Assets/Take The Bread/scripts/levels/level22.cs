using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class level22 : MonoBehaviour
{
    private void Start()
    {
       // Debug.Log(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameData.getInstance().isLock)//If the game locked,you can not control the game.
                return;
                other.GetComponent<Animator>().SetBool("run", false);
                other.GetComponent<MainCharacterMovement>().speed = 0;
                GameData.getInstance().main.gameWin();    //tell the engine the game wins.
           
            }
        }
    }

