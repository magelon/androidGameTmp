using UnityEngine;
using System.Collections;
using UnityEngine.UI;
////using DG.Tweening;
using UnityEngine.SceneManagement;
public class PanelMain : MonoBehaviour {


    public GameObject loadingImg;
    public Slider sl;
		// game UI elements
		public Text btnStart,btnMore,btnReview;
		//public GameObject titleCN,titleEN;
		public Toggle toggleMusic,toggleSFX;
		public Image mask;
		// Use this for initialization
		void Start () {
        //PlayerPrefs.DeleteAll();
        GameManager.getInstance().init();
 
				fadeOut ();


				toggleMusic.isOn = GameData.getInstance ().isSoundOn == 1 ? true : false;//0 is on
				toggleSFX.isOn = GameData.getInstance ().isSfxOn == 1 ? true : false;


				Localization.Instance.SetLanguage (GameData.getInstance().GetSystemLaguage());

				initView();
		}

		void Update () {
            Time.timeScale = 1;
		}
		public GameObject panelShop,panelFade;
		/// <summary>
		/// process kind of click events
		/// </summary>
		/// <param name="g">The green component.</param>
		public void OnClick(GameObject g){
				switch (g.name) {
				case "btnStart":
						GameManager.getInstance ().playSfx ("click");
                        ChangeScene("LevelMenu");

                        //fadeIn ("LevelMenu");
						break;
				case "btnMore":
						GameManager.getInstance().playSfx("click");
						if (Application.platform == RuntimePlatform.WP8Player) {


						} else {

								#if (UNITY_IPHONE || UNITY_ANDROID)
								Application.OpenURL ("https://play.google.com/store/apps/details?id=com.lon.war2"); 
								#endif
						}
						break;
            case "btnPrivacy":
                GameManager.getInstance().playSfx("click");
                if (Application.platform == RuntimePlatform.WP8Player)
                {


                }
                else
                {

#if (UNITY_IPHONE || UNITY_ANDROID)
                    Application.OpenURL("https://www.privacypolicies.com/privacy/view/f9557e9d4fa09efc36a7f4bd838723e1");
#endif
                }
                break;
            case "btnReview":
						GameManager.getInstance().playSfx("click");
						//			UniRate.Instance.RateIfNetworkAvailable();
						Application.OpenURL("itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id = "+Const.appid);
						break;
				case "btnShop":
						GameManager.getInstance().playSfx("click");
						panelShop.SetActive(true);
						break;
				case "btnGC":
						GameManager.getInstance().playSfx("click");
						GameManager.getInstance().ShowLeaderboard();
						break;
                case "Shop":
                GameManager.getInstance().playSfx("click");
                SceneManager.LoadScene("ShopMenu");
                break;
        }
		}


		void initView(){
				GameObject.Find ("btnStart").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnStart");
			//	GameObject.Find ("btnMore").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnMore");
			//	GameObject.Find ("btnReview").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnReview");
		}

		/// <summary>
		/// process toggle button(music and sound effect buttons)
		/// </summary>
		/// <param name="toggle">Toggle.</param>
		public void OnToggle(Toggle toggle){
				switch (toggle.gameObject.name) {
				case "ToggleMusic":
						GameManager.getInstance().playSfx("click");
						GameData.getInstance().isSoundOn = toggle.isOn ? 1 : 0;

						if(toggle.isOn){
								GameManager.getInstance().stopBGMusic();
						}else{
								GameManager.getInstance().playMusic("bgmusic");
						}
						PlayerPrefs.SetInt("sound",GameData.getInstance().isSoundOn);

						break;
				case "ToggleSfx":
						GameManager.getInstance().playSfx("click");
						GameData.getInstance().isSfxOn = toggle.isOn ? 1 : 0;
						if(toggle.isOn){
								GameManager.getInstance().stopAllSFX();
						}

						PlayerPrefs.SetInt("sfx",GameData.getInstance().isSfxOn);
						break;
				}
		}

    private void ChangeScene(string s)
    {
        
        StartCoroutine(LoadAsynchronously(s));
    }

    IEnumerator LoadAsynchronously(string s)
    {
        Time.timeScale = 1;
        loadingImg.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(s);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            sl.value = progress;
            yield return null;
        }
    }


    void fadeOut(){
				mask.gameObject.SetActive (true);
				mask.color = Color.black;

				ATween.ValueTo (mask.gameObject, ATween.Hash ("from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver","oncompletetarget",this.gameObject));

		}

		void fadeIn(string sceneName){

         if (mask.IsActive())
        	return;
        mask.gameObject.SetActive (true);
        mask.color = new Color(0,0,0,0);
        SceneManager.LoadScene(sceneName);
        ATween.ValueTo (mask.gameObject, ATween.Hash ("ignoretimescale", true,"from", 0, "to",1, "time",1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName,"oncompletetarget",this.gameObject));

    }


		void fadeInOver(string sceneName){
				SceneManager.LoadScene(sceneName);
		}

		void fadeOutOver(){
				mask.gameObject.SetActive (false);
		}

		/// <summary>
		/// tween update event
		/// </summary>
		/// <param name="value">Value.</param>
		void OnUpdateTween(float value)

		{

				mask.color = new Color(0,0,0,value);
		}
}
