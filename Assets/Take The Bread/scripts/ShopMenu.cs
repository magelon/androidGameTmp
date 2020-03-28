using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
////using DG.Tweening;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour {

		// Use this for initialization

		GameObject listItemg;
		GameObject mainContainer;
		List<GameObject> groups;

    private void Awake()
    {
        GameData.getInstance().resetData();
        GameManager.getInstance().init();
       
    }

    void Start () {
                 
				
        initView();
        Localization.Instance.SetLanguage (GameData.getInstance().GetSystemLaguage());
				
				mainContainer = GameObject.Find ("mainContainer");
				groups = new List<GameObject>();
				foreach (Transform group_ in mainContainer.transform) {
						groups.Add (group_.gameObject);
				}
		}

  

		bool isMoving = false;
		public void move(float dis){
				if (canmove) {
						foreach (Transform m in mainContainer.transform) {
								m.transform.Translate (dis, 0, 0);								
						}	
						isMoving = true;
				}
		}

		/// <summary>
		/// simulate Swipes the page to is right position
		/// </summary>
		/// <param name="force">Force.</param>
		public void swipePage(float force){


				if (Mathf.Abs(force) < 1f) {//user not do a quick swipe
						if (groups [page].transform.position.x < Screen.width / 4) {
								if (page >= 0 && page < pages) {
										GoRight ();
								} else {
										returnPage ();
								}

						} else if (groups [page].transform.position.x > Screen.width) {
								if (page <= pages && page > 0) {
										GoLeft ();
								} else {
										returnPage ();

								}
						} else {
								returnPage ();
						}

				} else {
						if (groups [page].transform.position.x < Screen.width / 2) {
								if (page >= 0 && page < pages) {
										GoRight ();
								} else {
										returnPage ();
								}

						} else if (groups [page].transform.position.x > Screen.width / 2) {
								if (page <= pages && page > 0) {
										GoLeft ();
								} else {
										returnPage ();

								}
						} else {
								returnPage ();
						}
				}

				//not allow level buttons active while moving the menu
				StopCoroutine ("swiped");
				StartCoroutine ("swiped");

		}

		//lock the game while page is auto moving.Unlock when finished
		IEnumerator swiped(){
				yield return new WaitForEndOfFrame ();
				isMoving = false;
		}

		public GameObject levelButton;//the level button template instance
		public GameObject dot;//the page dot for turn page

		int page = 0;//current page
		int pages = 1;//how many page
		public int perpage = 8;//icons per page
		List<GameObject> gContainer;//each icon group for per page
		List<GameObject> pageDots;//all page dots

		public Image mask;//the fade in/out mask

    public void initAgin()
    {
        initView();
    }

    //init all item datas 
		void initView(){
        float gap = transform.Find("bg").GetComponent<RectTransform>().rect.width;

        pageDots = new List<GameObject> ();


				pages = Mathf.FloorToInt (GameData.totalItem / perpage);
				for (int i = 0; i <= pages; i++) {
						GameObject tdot = Instantiate (dot, dot.transform.parent) as GameObject;
						tdot.SetActive (true);
						pageDots.Add (tdot);
						tdot.name = "dot_" + i;

				}

				setpageDot ();
				fadeOut ();

				gContainer = new List<GameObject>();
				gContainer.Add (levelButton.transform.parent.gameObject);
						levelButton.GetComponent<RectTransform> ().localScale = Vector3.one;
				Transform container = levelButton.transform.parent;
        //container.transform.localScale = Vector3.one;

        int n0 = 0;
        for (int i = perpage; i < GameData.totalItem; i += perpage)
        {

            GameObject tgroup = Instantiate(levelButton.transform.parent.gameObject, levelButton.transform.parent.parent) as GameObject;

            tgroup.transform.localPosition = new Vector3(gap * (n0 + 1), 0, 0);
          
            gContainer.Add(tgroup);
            n0++;
            //tgroup.transform.parent = levelButton.transform.parent.gameObject.transform.parent;
        }


        for (int i = 0; i < GameData.totalItem; i++) {
						GameObject tbtn = Instantiate (levelButton, Vector3.zero, Quaternion.identity) as GameObject;

						int tContainerNo = Mathf.FloorToInt (i / perpage);
                        tbtn.transform.SetParent(gContainer[tContainerNo].transform);
						//tbtn.transform.parent = gContainer[tContainerNo].transform;
						//			gContainer[tContainerNo].GetComponent<RectTransform> ().localScale = Vector3.one;
						tbtn.SetActive (true);

            tbtn.transform.localScale = new Vector3(1, 1, 1);
            //put item names
            tbtn.GetComponentInChildren<Text> ().text = "item"+i;
						//tbtn.transform.parent.localScale = Vector3.one;




						Text ttext = tbtn.GetComponentInChildren<Text> ();

            /*
						if (GameData.getInstance ().lvStar.Count > i+1) {

								int starCount = GameData.getInstance ().lvStar [i+1];


								if (GameData.getInstance ().lvStar.Count > i+1) {
										for (int j = 1; j <= starCount; j++) {
												ttext.transform.parent.Find ("star" + j).GetComponent<Image> ().enabled = true;
										}
								}
						}


						if (i >= GameData.getInstance ().levelPassed && i > 0) {

								ttext.enabled = false;
                
						} else {


								tbtn.name = "level"+(i+1);
								//tbtn.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (() => clickLevel (tbtn));
								ttext.gameObject.transform.parent.Find ("lock").GetComponent<Image> ().enabled = false;

						}**/

            //dispaly item lock condition
            tbtn.name = "item" + i;
            if (GameData.getInstance().itemLock[i] == 0)
            {
                ttext.gameObject.transform.parent.Find("lock").GetComponent<Image>().enabled = true;
            }
            else
            {
                ttext.gameObject.transform.parent.Find("lock").GetComponent<Image>().enabled = false;
                ttext.gameObject.transform.parent.Find("Image").
                    GetComponent<Image>().enabled = true;
                ttext.gameObject.transform.parent.Find("Image").
                    GetComponent<Image>().sprite = Resources.Load<Sprite>("hats/item"+ i);
                tbtn.GetComponentInChildren<Text>().text = "";
            }

				}


				//GameObject.Find ("txtScores").GetComponent<Text>().text = Localization.Instance.GetString("levelScore") + GameData.getInstance ().bestScore;
				//GameObject.Find ("confirm").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnContinue");


		}
		/// <summary>
		/// Clicks the dot. For turn page
		/// </summary>
		/// <param name="tdot">Tdot.</param>
		public void clickDot(GameObject tdot){
				int tdotIndex = int.Parse(tdot.transform.parent.name.Substring (4, tdot.transform.parent.name.Length - 4));
				page = tdotIndex;
				canmove = false;


				ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("ignoretimescale",true,"islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));


		}	

		/// <summary>
		/// page turned
		/// </summary>
		void dotclicked(){
				canmove = true;
				setpageDot ();

		}


		public static bool islock = false;
		/// <summary>
		/// Clicks the level button.
		/// </summary>
		/// <param name="tbtn">Tbtn.</param>
		void clickLevel(GameObject tbtn){
				if (!isMoving) {
						GameData.getInstance ().cLevel = int.Parse (tbtn.GetComponentInChildren<Text> ().text) - 1;
						fadeIn (tbtn.name);
            GameManager.getInstance().playSfx("click");
        }


		}


		/// <summary>
		/// Set dots for pages.
		/// </summary>
		void setpageDot(){
				for (int i = 0; i < pageDots.Count; i++) {
						pageDots [i].GetComponent<Image> ().color = new Color (1, 1, 1, .5f);
				}
				pageDots [page].GetComponent<Image> ().color = new Color (1, 1, 1, 1);
		}


		/// <summary>
		/// touch the continue to Continues your last level.
		/// </summary>
		public void continueLevel(){

				int tLastLevel = GameData.getInstance ().levelPassed;

				if (tLastLevel < GameData.totalItem) {
						GameData.getInstance ().cLevel = tLastLevel;
				} else {
						GameData.getInstance().cLevel = GameData.totalItem;
				}
		        if(GameData.getInstance().cLevel == 0)
        {
            GameData.getInstance().cLevel = 1;
        }
				string tstr = "level" + GameData.getInstance ().cLevel;
				fadeIn (tstr);

        GameManager.getInstance().playSfx("click");
    }

		/// <summary>
		/// Backs the main scene.
		/// </summary>
		public void backMain(){
				GameManager.getInstance ().playSfx ("click");
                
				fadeIn ("MainMenu");
		}

		/// <summary>
		/// Loads the game scene.
		/// </summary>
		public void loadGameScene(){

				SceneManager.LoadScene("Game"); 
		}
		/// <summary>
		/// Loads the main scene.
		/// </summary>
		public void loadMainScene(){

				SceneManager.LoadScene("MainMenu"); 
		}


		bool canmove = true;//can not enter a level and can not move when moving
		/// <summary>
		/// page Goes right.
		/// </summary>
		public void GoRight(){
				if (!canmove)
						return;
				if (page < pages) {

						page++;
						canmove = false;


						ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash( "ignoretimescale",true,"islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));

            GameManager.getInstance().playSfx("push");
        }
		}
		/// <summary>
		/// page goes left.
		/// </summary>
		public void GoLeft(){
				if (!canmove)
						return;
				if (page > 0) {

						page--;
						canmove = false;


						ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("ignoretimescale",true,"islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));

            GameManager.getInstance().playSfx("push");
        }
		}


		void fadeOut(){
				mask.gameObject.SetActive (true);
				mask.color = Color.black;

				ATween.ValueTo (mask.gameObject, ATween.Hash ("ignoretimescale",true,"from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver","oncompletetarget",this.gameObject));

		}

		void fadeIn(string sceneName){
				if (mask.IsActive())
						return;
				mask.gameObject.SetActive (true);
				mask.color = new Color(0,0,0,0);

				ATween.ValueTo (mask.gameObject, ATween.Hash ("ignoretimescale",true,"from", 0, "to", 1, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName,"oncompletetarget",this.gameObject));

		}


    /// <summary>
    /// when Fadein over.
    /// </summary>
    /// <param name="sceneName">Scene name.</param>
    void fadeInOver(string sceneName)
    {

        SceneManager.LoadScene(sceneName);
    }

		/// <summary>
		/// when fade out over
		/// </summary>
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
		/// <summary>
		/// Returns the page to its origin place.
		/// </summary>
		void returnPage(){
				canmove = false;
				ATween.MoveTo(gContainer[page].transform.parent.gameObject, ATween.Hash("ignoretimescale",true,"islocal", true,"x", -gContainer[page].transform.localPosition.x, "time",.3f,"easeType", "easeOutExpo", "oncomplete", "dotclicked","oncompletetarget",this.gameObject));

		}

		//debug use
		public void debugtext(string str){
				//				GameObject.Find ("txtScores").GetComponent<Text> ().text = str;
		}

}
