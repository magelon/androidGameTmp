using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSystem : MonoBehaviour {

    public int xp;
    public int currentlevel;

    private void Start()
    {
        xp = GameData.getInstance().bestScore;
    }

    public void UpdateXp() {
       
        int curlvl = (int)(0.1f * Mathf.Sqrt(xp));

        if (curlvl != currentlevel) {
            currentlevel = curlvl;
            //add some cool text to show level up
        }
        int xpnextlevel = 100 * (currentlevel + 1) * (currentlevel + 1);
        int differencexp = xpnextlevel - xp;

        int totaldifference = xpnextlevel - (100 * currentlevel * currentlevel);

        //differencexp/totaldifference
    }
	
	// Update is called once per frame
	void Update () {
        UpdateXp();
        Time.timeScale = 1;
	}
}
