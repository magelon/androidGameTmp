using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderBar : MonoBehaviour
{
    public GameObject gm;
    candycrush cc;
    public Slider sl;
    [SerializeField]
    private float slValue;
    bool win;

    void Start()
    {
        cc = gm.GetComponent<candycrush>();
    }
    
    void Update()
    {
        slValue = cc.score;
        sl.value = cc.score;
        if (!win)
        {
            Time.timeScale = 1;
        }
        if (slValue >= 200&&win==false)
        {
            win = true;
            GameData.getInstance().main.gameWin();
        }

    }
}
