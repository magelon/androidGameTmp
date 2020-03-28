using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level22Lose : MonoBehaviour
{
    public bool lose;
    private void Update()
    {
        if (lose)
        {
            GameData.getInstance().main.gameFailed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameData.getInstance().isLock)//If the game locked,you can not control the game.
                return;

                
                GameData.getInstance().main.gameFailed();    //tell the engine the game wins.
            }
        }
    }

