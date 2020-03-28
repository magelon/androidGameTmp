using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class StartSplash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //		PlayerPrefs.DeleteAll();
        //		MadLevelProfile.Reset ();
        //		bool istrial = UnityPluginForWindowsPhone.Class1.SwitchTrialMode();//comment this when on real version.

      



        SceneManager.LoadScene("DB");
	}
	

}
