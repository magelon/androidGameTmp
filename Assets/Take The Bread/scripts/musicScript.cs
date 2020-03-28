using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

//init admob
public class musicScript : MonoBehaviour {
	
	void Start () {
        //PlayerPrefs.DeleteAll();
		DontDestroyOnLoad (gameObject);
		asgroups = new List<AudioSource> ();
		StartCoroutine("recycle");
        
        #if UNITY_ANDROID
        string appId = "ca-app-pub-3308520213502941~2954898182";
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
        #else
            string appId = "unexpected_platform";
        #endif
         //Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
    }

    bool canRecycle = false;
	List<AudioSource> asgroups;
	IEnumerator recycle(){
		while (true) {
			yield return new WaitForSeconds(.1f);

			if(asgroups.Count > 30){
				for(int i = 0;i < 15;i++){

					Destroy(asgroups[0]);
					asgroups.RemoveAt(0);
				}
			}
		}
	}




	void OnApplicationPause(bool pauseStatus)
	{ 
//		Application.LoadLevel (Application.loadedLevelName);


//		if(!pauseStatus){
//			if(GameData.back2main % 3 == 0){
//				GameManager.getInstance ().showInterestitial ();
//				GameData.back2main++;
//			}
//
//		}


	}



    /*
void Update () {
    if (Input.GetKeyDown (KeyCode.Escape)) {
        Time.timeScale = 1;
        //			GameData.getInstance().init();
        //Debug.Log(Application.loadedLevelName);
        if (SceneManager.GetActiveScene().name.Substring(0, 5) == "level"){
            SceneManager.LoadScene("LevelMenu");
        }else if (SceneManager.GetActiveScene().name == "LevelMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }else if (SceneManager.GetActiveScene().name == "MainMenu")
        {

        }

        if (Application.loadedLevelName.Substring(0,5) == "level") {

            Application.LoadLevel ("LevelMenu");
        } else if(Application.loadedLevelName == "LevelMenu" ){

            Application.LoadLevel ("MainMenu");
        }else if(Application.loadedLevelName == "MainMenu"){


        }
       


        }
	}
    **/

	public AudioSource PlayAudioClip(AudioClip clip,bool isloop = false)
	{
		if (clip == null)return null;


		//		AudioSource source = (AudioSource)gameObject.GetComponent("AudioSource");
		//		if (source == null)

		AudioSource	source;

		if (isloop) {
			//bool tExist = false;
			AudioSource[] as1 = GetComponentsInChildren<AudioSource>();
			foreach(AudioSource tas in as1){
				if(tas && tas.clip){
					string clipname = (tas.clip.name);
					if(clipname == clip.name){
						source = tas;
						//tExist = true;
						source.Play();
						return source;
						break;
					}
				}
			}
		}

		source = (AudioSource)gameObject.AddComponent<AudioSource>();


		//		if (!tExist) {
		//			source = (AudioSource)gameObject.AddComponent<AudioSource>();
		//		}



		source.clip = clip;source.minDistance = 1.0f;source.maxDistance = 50;source.rolloffMode = AudioRolloffMode.Linear;
		source.transform.position = transform.position;
		source.loop = isloop;
		source.Play();
		if (!isloop) {//not bg
			asgroups.Add (source);
		}
		return source;
	}

}
