using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
public class GameData  {

		// Use this for initialization

		public int levelPassed = 0;
		public int cLevel = 0;
		public int bestScore = 0;
		public long cScore = 0;
		public int isSoundOn = 0;//is music on
		public int isSfxOn = 0;//is sound effect on
		public static bool isTrial;//for wp8,not used
		public static string lastWindow = ""; // remember last scene type
		public static bool isInGame = false; //not used
		public static bool isAds = true;//whether show advertisment
	
		public int tipRemain = 0;//not used
		public int coin = 0;//your coin

		public MainScript main;//mainscipt instance
		public GameObject maing;//not used
    //config how many level there are for level menu usage
		public static int totalLevel = 1409;//total levels
    //config for items
        public static int totalItem = 10;

    public List<int> lvStar = new List<int>(260);

    //items lock or not
    public List<int> itemLock = new List<int>(260);


    //control
    public int controlType = 1;//not used


		public bool startgame =false;//may use for wait user to start the game.Not used
		public bool isDead =false;//not used
	
        //singleton gameData
		public static GameData instance;
		public static GameData getInstance(){
				if (instance == null) {
						instance = new GameData();
                        //PlayerPrefs.DeleteAll ();
				}
				return instance;
		}

		public bool isWin = false;//game is at win state
		public bool isFail = false;//game is at fail state
		private bool islock = false;//game is at lock state

		public string tickStartTime = "0";
		

		public bool cLvShowedTip = false;


		public void resetData(){

                itemLock = new List<int>();
                islock = false;

				isWin = false;
				isFail = false;

				tipRemain = PlayerPrefs.GetInt ("tipRemain", 1);
				tickStartTime = PlayerPrefs.GetString ("tickStartTime", "0");
				if (tickStartTime == "0") {
						TimeSpan ts=new TimeSpan(50,0,0,0);  
						DateTime dt2=DateTime.Now.Subtract(ts); 
						long cTime = dt2.Ticks/10000000;	
						tickStartTime = cTime.ToString();;
						PlayerPrefs.SetString ("tickStartTime", tickStartTime);

				}

		
				cLvShowedTip = false;

				//game
				startgame = false;
			
				isDead = false;
		}
		/// <summary>
		/// Gets the level best score.When you play a level,you see your score at the end of the game.
		/// This game score is 600-the time you cost in game.So give the least time(seconds)to each level below as the best score
		/// </summary>
		/// <returns>The level best score.</returns>
		public int getLevelBestScore(){
				int tbestScore = 10;
				//switch (Application.loadedLevelName) {

                switch (SceneManager.GetActiveScene().name) { 
                case "level1":
						tbestScore = 2;
						break;
				case "level2":
						tbestScore = 3;
						break;
				case "level3":
						tbestScore = 3;
						break;
				case "level4":
						tbestScore = 13;
						break;
				case "level5":
						tbestScore = 3;
						break;
				case "level6":
						tbestScore = 11;
						break;
				case "level7":
						tbestScore = 13;
						break;
				case "level8":
						tbestScore = 5;
						break;
				case "level9":
						tbestScore = 4;
						break;
				case "level10":
						tbestScore = 10;/////
						break;
				case "level11":
						tbestScore = 4;
						break;
				case "level12":
						tbestScore = 3;
						break;
				case "level13":
						tbestScore = 4;
						break;
				case "level14":
						tbestScore = 6;
						break;
				case "level15":
						tbestScore = 9;
						break;
				case "level16":
						tbestScore = 5;
						break;
				case "level17":
						tbestScore = 6;
						break;
				case "level18":
						tbestScore = 9;
						break;
				case "level19":
						tbestScore = 3;
						break;
				case "level20":
						tbestScore = 2;
						break;
				case "level21":
						tbestScore = 10;
						break;
				default:
						tbestScore = 10;	
						break;

				}
				return tbestScore;
		}

		/// <summary>
		/// Locks the game.
		/// </summary>
		/// <param name="_lock">If set to <c>true</c> lock.</param>
		/// <param name="stopTime">If set to <c>true</c> stop time.</param>
		public void lockGame(bool _lock,bool stopTime = true){
				islock = _lock;
				if (stopTime) {
						Time.timeScale = islock ? 0 : 1;
				}
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="GameData"/> is lock.
		/// </summary>
		/// <value><c>true</c> if is lock; otherwise, <c>false</c>.</value>
		public bool isLock{
				get{
						return islock;	
				}
		}

		/// <summary>
		/// Gets the system laguage.
		/// </summary>
		/// <returns>The system laguage.</returns>
		public int GetSystemLaguage(){
				int returnValue = 0;
				switch (Application.systemLanguage) {
				case SystemLanguage.Chinese:
						returnValue = 1;
						break;
				case SystemLanguage.ChineseSimplified:
						returnValue = 1;
						break;
				case SystemLanguage.ChineseTraditional:
						returnValue = 1;
						break;
				default:
						returnValue = 0;
						break;

				}
				returnValue = 0;//test
				return returnValue;
		}






}
