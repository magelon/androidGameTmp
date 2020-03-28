using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager{
		public static GameManager instance;
		public static GameManager getInstance(){
				if(instance == null){
						instance = new GameManager();
						instance.init ();
				}
				return instance;
		}

		GameObject music;//sound control instance
		/// <summary>
		/// Plaies the music.
		/// </summary>
		/// <param name="str">String.</param>
		/// <param name="isforce">If set to <c>true</c> isforce.</param>
		public void playMusic(string str,bool isforce = false){

				//do not play the same music againDebug.Log (musicName+"__"+str);
				if (!isforce) {
						if (bgMusic != null && musicName == str) {
								return;
						}
				}


				if (!music)
						return;

				AudioSource tmusic = null;

				AudioClip clip = (AudioClip)Resources.Load ("sound\\"+str, typeof(AudioClip));//调用Resources方法加载AudioClip资源

				Debug.Log (clip);
				if (GameData.getInstance ().isSoundOn == 0) {
						if (bgMusic)
								bgMusic.Stop ();
						tmusic = music.GetComponent<musicScript> ().PlayAudioClip (clip,true);
						if (str.Substring (0, 2) == "bg") {
								musicName = str;
								bgMusic = tmusic;
						}
				}
		}


		List<AudioSource> currentSFX = new List<AudioSource>();
		Dictionary<string,int> sfxdic = new Dictionary<string,int>();

		AudioSource cWalk = new AudioSource (); //sometime for continous sound like walk steps.
		/// <summary>
		/// Plaies the sfx.
		/// </summary>
		/// <returns>The sfx.</returns>
		/// <param name="str">String.</param>
		public AudioSource playSfx(string str){
				AudioSource sfxSound = null;

				if (!music)
						return null;
				//				if (sfxdic.ContainsKey("walk") && sfxdic["walk"] == 1 && str == "walk") {
				//						
				//				}
				AudioClip clip = (AudioClip)Resources.Load ("sound\\"+str, typeof(AudioClip));//调用Resources方法加载AudioClip资源
				if (GameData.getInstance ().isSfxOn == 0) {
						sfxSound = music.GetComponent<musicScript> ().PlayAudioClip (clip);
						if (sfxSound != null) {
								if (sfxdic.ContainsKey (str) == false || sfxdic [str] != 1) {
										currentSFX.Add (sfxSound);

										sfxdic [str] = 1;
										if(str == "walk"){
												cWalk = sfxSound;
										}
								}
						}	
				}	

				return sfxSound;
		}
		AudioSource bgMusic = new AudioSource();//record background music
		public string musicName = "";
		/// <summary>
		/// Stops the background music.
		/// </summary>
		public void stopBGMusic(){
				if(bgMusic){
						bgMusic.Stop();
						musicName = "";
				}
		}
		/// <summary>
		/// Stops all sound effect.
		/// </summary>
		public void stopAllSFX(){
				foreach(AudioSource taudio in currentSFX){
						if(taudio!=null)taudio.Stop();
				}
				currentSFX.Clear ();
				sfxdic.Clear ();
		}

		/// <summary>
		/// detect a certain sound whether is playing
		/// </summary>
		/// <returns><c>true</c>, if playing sfx was ised, <c>false</c> otherwise.</returns>
		/// <param name="str">String.</param>
		public bool isPlayingSfx(string str){
				bool isPlaying = false;
				if (sfxdic.ContainsKey(str) && sfxdic [str] == 1) {
						isPlaying = true;
				}
				return isPlaying;
		}
		/// <summary>
		/// Stops the music.
		/// </summary>
		/// <param name="musicName">Music name.</param>
		public void stopMusic(string musicName = ""){
				if (music) {
						AudioSource[] as1 = music.GetComponentsInChildren<AudioSource> ();
						foreach (AudioSource tas in as1) {
								if(musicName == ""){
										tas.Stop ();
										break;
								}else{
										if(tas && tas.clip){
												string clipname = (tas.clip.name);
												if(clipname == musicName){
														tas.Stop();


														musicName = "";
														if(sfxdic.ContainsKey(clipname)){
																sfxdic[clipname] = 0;
																if (clipname == "walk") {
																		if (cWalk != null) {
																				cWalk.Stop ();
																				cWalk = null;
																		}
																}
														}
														break;
												}
										}
								}
						}
				}
		}

		/// <summary>
		/// switch the sound.
		/// </summary>
		public void toggleSound(){	 
				int soundState  = GameData.getInstance().isSoundOn;
		}
		/// <summary>
		/// Submits the game center.
		/// </summary>
		public void submitGameCenter(){
				if(!isAuthored) {
						//Debug.Log("authenticating...");
						//initGameCenter();
				}else{
						Debug.Log("submitting score...");
						//			int totalScore = getAllScore();
						int tbestScore = GameData.getInstance().bestScore;			
						ReportScore(Const.LEADER_BOARD_ID,tbestScore);
				}
		}
		public void init(){
				GameData.getInstance ().resetData ();
								//PlayerPrefs.DeleteAll ();
				music = GameObject.Find("music") as GameObject;
				int allScore = 0;
				for(int i = 1;i<=GameData.totalLevel;i++){
						int tScore = PlayerPrefs.GetInt("levelScore_"+i.ToString(),0);
						allScore += tScore;
//						Debug.Log("=========================bestScore is:"+tScore);
				}

                //set how many level passed and smart way
				GameData.getInstance().levelPassed = PlayerPrefs.GetInt("levelPassed",0);
				Debug.Log ("current passed level = " + GameData.getInstance ().levelPassed);
				for (int i = 0; i<=GameData.getInstance().levelPassed; i++) {
//						MadLevelProfile.SetCompleted ("Level "+(i), true);
				}

                //set all level start condition
				for(int i = 0;i<=GameData.totalLevel;i++){

						//save star state to gameobject
						int tStar = PlayerPrefs.GetInt("levelStar_"+i.ToString(),0);
						GameData.getInstance().lvStar.Add(tStar);
//						Debug.Log ("=============================xxxx" + i+"ss"+tStar);
				}


        //inite each item lock condition
        for (int i = 0; i <= GameData.totalItem; i++)
        {
            //int value of each item defalut 0
            int tStar = PlayerPrefs.GetInt("item_" + i.ToString(), 0);
            GameData.getInstance().itemLock.Add(tStar);
        }
                GameData.getInstance().bestScore = allScore;
				GameData.getInstance().isSoundOn = (int)PlayerPrefs.GetInt("sound",0);
				GameData.getInstance().isSfxOn = (int)PlayerPrefs.GetInt("sfx",0);
				Debug.Log("soundstate:"+GameData.getInstance().isSoundOn+"sfxstate:"+GameData.getInstance().isSfxOn);
				initGameCenter();
		}
		public bool noToggleSound = false;
		/// <summary>
		/// Sets the state of the toggle buttons.
		/// </summary>
		public void setToggleState(){
				//this section will trigger the click itself.So force not play the sound.(if notogglesound is true)
				noToggleSound = true;
				GameObject checkMusicG = GameObject.Find ("toggleMusic");
				if (checkMusicG) {
						noToggleSound = false;
				}
		}
		//=================================GameCenter======================================
		public void initGameCenter(){
				//Social.localUser.Authenticate(HandleAuthenticated);
		}
		private bool isAuthored = false;
		private void HandleAuthenticated(bool success)
		{
				//        Debug.Log("*** HandleAuthenticated: success = " + success);
				if (success) {
						Social.localUser.LoadFriends(HandleFriendsLoaded);
						Social.LoadAchievements(HandleAchievementsLoaded);
						Social.LoadAchievementDescriptions(HandleAchievementDescriptionsLoaded);
						isAuthored = true;
						//登录成功就提交分数
						submitGameCenter();
				}
		}

		private void HandleFriendsLoaded(bool success)
		{
				//        Debug.Log("*** HandleFriendsLoaded: success = " + success);
				foreach (IUserProfile friend in Social.localUser.friends) {
						//            Debug.Log("*   friend = " + friend.ToString());
				}
		}

		private void HandleAchievementsLoaded(IAchievement[] achievements)
		{
				//        Debug.Log("*** HandleAchievementsLoaded");
				foreach (IAchievement achievement in achievements) {
						//            Debug.Log("*   achievement = " + achievement.ToString());
				}
		}

		private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)
		{
				//        Debug.Log("*** HandleAchievementDescriptionsLoaded");
				foreach (IAchievementDescription achievementDescription in achievementDescriptions) {
						//            Debug.Log("*   achievementDescription = " + achievementDescription.ToString());
				}
		}

		// achievements

		public void ReportProgress(string achievementId, double progress)
		{
				if (Social.localUser.authenticated) {
						Social.ReportProgress(achievementId, progress, HandleProgressReported);
				}
		}

		private void HandleProgressReported(bool success)
		{
				//        Debug.Log("*** HandleProgressReported: success = " + success);
		}

		public void ShowAchievements()
		{
				if (Social.localUser.authenticated) {
						Social.ShowAchievementsUI();
				}
		}

		// leaderboard

		public void ReportScore(string leaderboardId, long score)
		{
				Debug.Log("submitting score to GC...");
				if (Social.localUser.authenticated) {
						Social.ReportScore(score, leaderboardId, HandleScoreReported);
				}
		}

		public void HandleScoreReported(bool success)
		{
				//        Debug.Log("*** HandleScoreReported: success = " + success);
		}

		public void ShowLeaderboard()
		{
				Debug.Log("showLeaderboard");
				if (Social.localUser.authenticated) {
						Social.ShowLeaderboardUI();
				}
		}

		
}
