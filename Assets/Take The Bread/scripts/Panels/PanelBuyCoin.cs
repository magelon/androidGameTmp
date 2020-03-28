using UnityEngine;
using System.Collections;
// using SmartLocalization;
using UnityEngine.UI;
public class PanelBuyCoin : MonoBehaviour {

		// Use this for initialization
		GameObject scrollpanel;
		string lang = "";
		public GameObject btnBuyCoinClose;
		public GameObject panelBuyAlert;
        public GameObject spinButton;
		GameObject panel;
		void Start () {

				lang = "en";

		}

		// Update is called once per frame
		void Update () {

		}

		public void showMe(){
				panel = transform.Find ("panel").gameObject;	
				initView ();
				GameData.getInstance ().lockGame (true);
				ATween.MoveTo (panel, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

		}

		void initView(){


				panel.transform.Find ("title").GetComponent<Text> ().text = Localization.Instance.GetString ("titleShop");
				panel.transform.Find ("btnClose").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnClose");

				for (int i = 0; i<4; i++) {
						GameObject trow = GameObject.Find ("row" + i);
						trow.transform.Find("lbDetail").GetComponent<Text>().text = Localization.Instance.GetString("price"+(i+1)+"tip");
						trow.transform.Find("lbPrice").GetComponent<Text>().text = Localization.Instance.GetString("price"+(i+1));
				}
		}


		IEnumerator hideWait(){
				yield return new WaitForSeconds (30);
				panelBuyAlert.SetActive(false);
		}


		public void OnClick2(GameObject g){
				switch (g.name) {
				case "btnBuyCoin":
						GameManager.getInstance().playSfx("select");

						int tindex = int.Parse(g.transform.parent.name.Substring(3,1));
						print(tindex);
                //add coin and dispaly
                //GameData.getInstance().coin += 60;
                //PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                //GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                //GameManager.getInstance().buy(tindex);
                break;
				case "btnClose":

						panel = transform.Find ("panel").gameObject;	
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 600, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject,"oncompleteparams", "buyClose"));
						break;
				}
		}


		void OnHideCompleted(string str){
				switch (str) {
				case "buyClose":
						gameObject.SetActive (false);
						GameData.getInstance ().lockGame (false);
                        if (spinButton)
                        {
                            spinButton.SetActive(true);
                        }
                       
						break;
				}

		}
}
