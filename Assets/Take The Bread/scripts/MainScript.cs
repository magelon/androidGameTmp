using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//using Umeng;

using UnityEngine.SceneManagement;
public class MainScript : MonoBehaviour {

    private rewardVideo rv;
    private InterstitialAdScript inter;
    private ReInterstitialScript reinter;
    private bool intershowed;
    private int adFree=0;
		//data
		public int timeCount = 0;
		public Text txtCoin;
    public Text txtLevel;
    public GameObject spinButton;
        int clevel;

    private GameObject winPartical;

        void Start () {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("adFree",0)!=0)
        {
            adFree = PlayerPrefs.GetInt("adFree");
        }

				initData ();
				initView ();
        rv = GetComponent<rewardVideo>();
        inter = GetComponent<InterstitialAdScript>();
        reinter = GetComponent<ReInterstitialScript>();
        StartCoroutine("waitAsecond");
        Time.timeScale = 0;
		}

		IEnumerator waitAsecond(){
				while (true) {
						yield return new WaitForSeconds (1);

						timeCount++;
				}
		}

		

		public PanelDisplayTip panelDisplayTip;//panel instance for display tips.



        //public GameObject panelAskGacha;//panel instance for Ask Gacha


		public GameObject panelAskTip;//panel instance for ask for tip
		public GameObject panelAskSkip;//panel instance for ask for skip
		public GameObject panelNotEnough;//panel instance for wanning you not enough
		public GameObject panelBuyCoin;//panle instance for buy coin
		void initView(){
				GameData.lastWindow = "game";
                string levelname = SceneManager.GetActiveScene().name;// Application.loadedLevelName;
                if (levelname == "ShopMenu")
        {
            clevel = 999999;
        }
        else
        {
            clevel = int.Parse(levelname.Substring(5, levelname.Length - 5));
        }
				
				GameData.getInstance ().cLevel = GameData.getInstance().levelPassed;

				clevel=GameData.getInstance ().cLevel;

				//txtLevel.text = Localization.Instance.GetString ("levelname" + clevel);
        txtLevel.text = "level" + clevel;

        fadeOut ();


				GameObject panelBuyCoinC = GameObject.Find ("PanelBuyCoinsC");

				

				initGameView (); //request

        }
		/// <summary>
		/// Inits the game view.
		/// </summary>
		void initGameView(){
				//int tnFloor = (int)Random.Range (1, 5);
				//int tnWall = (int)Random.Range (1, 4);
				//GameObject tFloor = Resources.Load ("floor" + tnFloor) as GameObject;
				//GameObject tWall = Resources.Load ("wall" + tnWall) as GameObject;
				//GameObject tcorner = Resources.Load ("cornerbar") as GameObject;
				//tFloor = Instantiate(tFloor,new Vector3(0,-2.5f,0),Quaternion.identity) as GameObject;
				//tWall = Instantiate(tWall,new Vector3(0,.5f,0),Quaternion.identity) as GameObject;
				//tcorner = Instantiate (tcorner, new Vector3 (0, -2, 0), Quaternion.identity) as GameObject;
				//if (Camera.main.transform.position.y == 1) {
						//tFloor.transform.position = new Vector3(0,-1.5f,0);
						//tWall.transform.position = new Vector3(0,1.5f,0);
						//tcorner.transform.position = new Vector3(0, -1, 0);
				//}

				txtCoin.text = GameData.getInstance ().coin.ToString ();
				if (GameData.getInstance ().cLevel >= GameData.totalLevel) {
                 if (GameObject.Find("btnSkip"))
                 {
                     GameObject.Find("btnSkip").SetActive(false);
                    }
						
				}
		}


		public void refreshView(){

		}
        

		GameObject gameContainer;
		void initData(){
				
				GameData.getInstance ().main = this;
				GameData.getInstance ().resetData ();
				Time.timeScale = 1;
				gameContainer = GameObject.Find("gameContainer");
				GameData.getInstance ().coin = PlayerPrefs.GetInt ("coin");

				//Localization.Instance.SetLanguage (GameData.getInstance().GetSystemLaguage());

		}
        

		public GameObject panelWin;

		public void gameWin(bool directWin = false){
				if (GameData.getInstance ().isFail)
						return;
				GameData.getInstance ().lockGame(true,false);

				
        if (winPartical == null)
        {
            GameManager.getInstance().playSfx("applaud");
            winPartical = Instantiate(Resources.Load("P_Confetti") as GameObject);
        }      
                StartCoroutine ("win");
                
    }

		IEnumerator win(){
				yield return new WaitForSeconds (1);

	
				if (GameData.getInstance().cLevel < GameData.totalLevel) {
						panelWin.transform.Find("panel").transform.Find ("btnTitle").gameObject.SetActive (false);
				} else {
						panelWin.transform.Find("panel").transform.Find ("btnNext").gameObject.SetActive (false);
				}
	
				GameData.getInstance ().isWin = true;
                int tclv = int.Parse(SceneManager.GetActiveScene().name.
                Substring(5, SceneManager.GetActiveScene().name.Length-5));
				//if (tclv % 3 == 1 && tclv > 6) {
					//	GameManager.getInstance ().ShowInterestitial ();
				//}
			

				panelWin.SetActive(true);
				Transform panelWin_ = panelWin.transform.Find ("panel");
				ATween.MoveTo (panelWin_.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

				panelWin_.transform.Find ("wintitle").GetComponent<Text> ().text = Localization.Instance.GetString ("wintitle");

				panelWin_.transform.Find ("btnTitle").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnTitle");

				disableAll ();



				int tScore = 600-timeCount;//600 - timeCount - (myStep + myUndo - minStep)*2;
				if (tScore < 0)
						tScore = 0;
				//		lbScore.Text = LanguageManager.Instance.GetTextValue("GAME_SCORE") +": " + tScore;

				panelWin_.transform.Find ("levelscore").GetComponent<Text> ().text = Localization.Instance.GetString ("levelscore")+" "+tScore;
				int tbestScore = GameData.getInstance ().getLevelBestScore ();
				int nStar = 0;
				//				print (timeCount);
				if (timeCount <= tbestScore) {
						nStar = 3;		
				} else if (timeCount <= tbestScore + 3) {
						nStar = 2;		
				} else if (timeCount <= tbestScore + 5) {
						nStar = 1;
				} else {
						nStar = 0;
				}





				for(int i = 1;i<=3;i++){
						GameObject tstar = GameObject.Find("star"+i);
						if(i <= nStar){
								tstar.GetComponent<Image>().enabled = true;
						}else{
								tstar.GetComponent<Image>().enabled = false;
						}
				}

				//save
				int saveLevel = 0;

				if (GameData.getInstance ().cLevel < GameData.totalLevel) {
						saveLevel = GameData.getInstance ().cLevel + 1;

                       


                } else {


				}
				//		print (GameData.getInstance ().levelPassed);
				if (GameData.getInstance ().levelPassed < saveLevel) {
						print ("saving..");
						PlayerPrefs.SetInt("levelPassed",saveLevel);
						GameData.getInstance().levelPassed = saveLevel;
            //reward for pass level
            PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + 20);
            txtCoin.text = PlayerPrefs.GetInt("coin") + "";
            PlayerPrefs.SetInt("tipRemain",GameData.getInstance().tipRemain);
						PlayerPrefs.Save();
				}


				//save score
				int cLvScore = PlayerPrefs.GetInt ("levelScore_"+GameData.getInstance ().cLevel, 0);
				//		print (cLvScore + "_" + timeCount);
				if (cLvScore < tScore) {
						PlayerPrefs.SetInt ("levelScore_"+GameData.getInstance ().cLevel, tScore);
						print (tScore+"tallscore "+GameData.getInstance().cLevel);
						//save to GameData instantlly
						//			print(GameData.getInstance().lvStar.Count+"    "+GameData.getInstance().cLevel);
						if(GameData.getInstance().lvStar.Count != 0){
								GameData.getInstance().lvStar[GameData.getInstance ().cLevel] = nStar;
								//			print ("save new score"+cLvScore+"_"+timeCount);


								//submitscore
								int tallScore = 0;
								for(int i = 0;i<GameData.totalLevel;i++){
										int tlvScore = PlayerPrefs.GetInt("levelScore_"+i.ToString(),0);
										tallScore += tlvScore;

								}

								GameData.getInstance().bestScore = tallScore;
								GameManager.getInstance().submitGameCenter();

						}

						//check star
						int cLvStar = PlayerPrefs.GetInt ("levelStar_"+GameData.getInstance ().cLevel, 0);
						//		print ("getstar"+cLvStar+"   "+nStar);
						if (cLvStar < nStar) {

								PlayerPrefs.SetInt ("levelStar_"+GameData.getInstance ().cLevel, nStar);
								for (int i = 1; i<=nStar; i++) {

								}
						}

				
				}
		}

		static int nFail = 0;
		public GameObject panelFail;
		public void gameFailed(){

				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
						return;

				GameData.getInstance ().lockGame (true,false);
				GameManager.getInstance ().playSfx ("gamelose");



				GameData.getInstance ().isFail = true;
				//		GA.FailLevel(Application.loadedLevelName);
				//		GameData.getInstance ().isWin = true;
				nFail++;
				if (nFail == 5) {
						//GameManager.getInstance ().ShowInterestitial ();
						nFail = 0;
				}


				StartCoroutine ("fail");

		}
		IEnumerator fail(){
				yield return new WaitForSeconds (1);
		
				panelFail.SetActive(true);
				Transform panelFail_ = panelFail.transform.Find ("panel");
				ATween.MoveTo (panelFail_.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

				panelFail_.transform.Find ("failTitle").GetComponent<Text> ().text = Localization.Instance.GetString ("failtitle");
				panelFail_.transform.Find ("btnTitle").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnTitle");
				panelFail_.transform.Find ("btnRetry").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnRetry");

				disableAll ();
		}



		/// <summary>
		/// Deal with button actions
		/// </summary>
		/// <param name="g">The green component.</param>
		public void buttonHandler(GameObject g){

				switch(g.name){
				case "btnMain":

						GameManager.getInstance ().playSfx ("click");
						break;
				case "btnLevel":

						GameManager.getInstance ().playSfx ("click");
						GameData.isInGame = false;
						break;
				case "btnRewind":

				case "btnRestart":
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        //Application.LoadLevel(Application.loadedLevelName);
						GameManager.getInstance ().playSfx ("click");
						break;
				case "btnRetry":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                GameManager.getInstance ().playSfx ("click");
						break;
				case "btnContinue":
						if(GameData.getInstance().cLevel < GameData.totalLevel-1){
								GameData.getInstance().cLevel++;
						}SceneManager.LoadScene("level" + (GameData.getInstance().cLevel + 1));
						GameManager.getInstance ().playSfx ("click");
						break;
				case "btnAction":
						GameData.getInstance().maing.BroadcastMessage("action");
						break;
				case "btnPauseClose":

						Time.timeScale = 1;
						break;
				case "btnHelp":
						if(GameData.getInstance().isWin || GameData.getInstance().isFail)return;


						break;
				case "btnTitle":

						GameManager.getInstance ().playSfx ("click");
						GameData.isInGame = false;
						break;
				case "btnPause":
						Time.timeScale = 0;
						if(GameData.getInstance().isWin || GameData.getInstance().isFail)return;


						break;
				case "btnTip":
						if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
								return;
						
						if (GameData.getInstance ().isLock)
								return;
						
						panelAskTip.SetActive (true);
						panelAskTip.GetComponent<PanelAskTip> ().showMe ();
						GameManager.getInstance ().playSfx ("click");
					
						break;
				case "btnSkip":
						if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
								return;
						GameManager.getInstance ().playSfx ("click");
						if (GameData.getInstance ().isLock)
								return;
						panelAskSkip.SetActive (true);
						panelAskSkip.GetComponent<PanelAskSkip> ().showMe ();
						break;
				case "btnMoneyBuy":
						if(GameData.getInstance().isWin || GameData.getInstance().isFail)return;

						GameData.getInstance().lockGame(true);
						GameManager.getInstance().playSfx("click");
			
						GameObject.Find ("PanelAskSkip").GetComponent<PanelAskSkip>().showMe();
						panelDisplayTip.showMe();
						break;
				case "btnNotEnoughCoin":
						if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
								return;
					
						GameData.getInstance ().lockGame (true);
						GameManager.getInstance().playSfx("click");
						break;
				case "btnSkipMenu":
						if(GameData.getInstance().isWin || GameData.getInstance().isFail)return;
						GameManager.getInstance().playSfx("click");
						GameObject.Find ("PanelAskSkip").GetComponent<PanelAskSkip>().showMe();

						break;
                case "SpinButton":
                 
                panelAskSkip.SetActive(true);
                panelAskSkip.GetComponent<PanelAskSkip>().showMe();
                break;
        }

		}


		/// <summary>
		/// Loads the main scene.
		/// </summary>
		public void loadMainScene(){
        SceneManager.LoadScene("MainMenu");
               
		}
		/// <summary>
		/// Loads the level scene.
		/// </summary>
		public void loadLevelScene(){
        SceneManager.LoadScene("LevelMenu");

		}


		
		//--------------------


		void disableAll(){
				GameObject tipWindow = GameObject.Find("panelHelp");
				if (tipWindow != null) {

				}
		}


		public void OnClick(GameObject g){
				switch (g.name) {
				case "btnBuyCoin":
						panelBuyCoin.SetActive (true);
						panelBuyCoin.GetComponent<PanelBuyCoin> ().showMe ();
                //hide gacha mechina
                if (spinButton)
                {
                    spinButton.SetActive(false);
                }

                break;
				case "btnLevel":
						fadeIn ("LevelMenu");
						break;
				case "btnTitle":
						fadeIn ("MainMenu");
						break;
				case "btnRetry":
                if (adFree == 1)
                {
                    intershowed = true;
                    fadeIn(SceneManager.GetActiveScene().name);

                }
                else
                {
                    fadeIn(SceneManager.GetActiveScene().name);
                    //reinter.showAd();
                }

                break;
				case "btnNext":
                        nextLevel();
						//fadeIn ("level"+(clevel+1));
						break;
                case "rewardVideo":
                        rv.ShowRewardBasedVideo();
                        break;
                }
		}

        public void nextLevelSkip()
    {
        //fadeIn("level" + (clevel + 1));
		fadeIn("level1");
    }

		public void nextLevel(){
        if (adFree == 1)
        {
            intershowed = true;   
        }
        if (clevel % 2 == 0&&!intershowed)
        {
            inter.showAd();
            if (!intershowed)
            {
                intershowed = true;
            }
        }
        else if(clevel%2==0&&intershowed)
        {
            //fadeIn("level" + (clevel + 1));
			fadeIn("level1");
        }
        else
        {
            //fadeIn("level" + (clevel + 1));
			fadeIn("level1");
        }


    }

		/// <summary>
		/// camera fade out
		/// </summary>
		public Image mask;
		void fadeOut(){
				mask.gameObject.SetActive (true);
				mask.color = Color.black;
				ATween.ValueTo (mask.gameObject, ATween.Hash ("ignoretimescale",true,"from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver","oncompletetarget",this.gameObject));

		}
		/// <summary>
		/// camera fade in
		/// </summary>
		/// <param name="sceneName">Scene name.</param>
		public void fadeIn(string sceneName){
				if (mask.IsActive())
						return;
				mask.gameObject.SetActive (true);
				mask.color = new Color(0,0,0,0);

				ATween.ValueTo (mask.gameObject, ATween.Hash ("ignoretimescale",true,"from", 0, "to", 1, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName,"oncompletetarget",this.gameObject));

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
