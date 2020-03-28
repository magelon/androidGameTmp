using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyBouns : MonoBehaviour
{
    string date;
    int dayCount;
    public RectTransform panel;
    private int bonus;

    private void Awake()
    {
        date = DateTime.Now.ToString().Substring(0, 9);
        if (PlayerPrefs.GetInt(date) == 2)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        dayCount = PlayerPrefs.GetInt("dayCount", 0);
        bonus = panel.childCount;
        date = DateTime.Now.ToString().Substring(0, 9);
        Debug.Log(date);

        if (!PlayerPrefs.HasKey(date))
        {
            PlayerPrefs.SetInt(date, 1);
            int coin = PlayerPrefs.GetInt("coin");
            coin = coin + 120;
            PlayerPrefs.SetInt("coin", coin);
            PlayerPrefs.SetInt(date, 2);
            dayCount++;
            PlayerPrefs.SetInt("dayCount", dayCount);
            //get daily Bouns
            for (int i = 0; i < dayCount; i++)
            {
                if (i < dayCount - 1)
                {
                   panel.GetChild(i).GetChild(1).gameObject.SetActive(true);
                }

                if (i == dayCount - 1)
                {
                    panel.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    //add 120 to prefs
                }
            }

            if (dayCount == bonus)
            {
                //clean up
                PlayerPrefs.SetInt("dayCount", 0);
            }
        }
        else
        {
            for (int i = 0; i < dayCount; i++)
            {
                panel.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }

        }
        StartCoroutine(gohome());
    }
    IEnumerator  gohome()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
