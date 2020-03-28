using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PanelNotEnough : MonoBehaviour {

		public GameObject panel;
		public GameObject panelBuyCoin;
        public GameObject gachaButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		public void OnClick(GameObject g){
				switch (g.name) {
				case "btnBuyCoin":
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted2", "oncompletetarget", this.gameObject));
			
						panelBuyCoin.SetActive (true);
						panelBuyCoin.GetComponent<PanelBuyCoin> ().showMe ();
						break;

				case "btnCancel":
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                if (gachaButton)
                {
                    gachaButton.SetActive(true);
                }
                break;

				}
		}


		public void showMe(){

				panel.transform.Find ("tiptitle").GetComponent<Text> ().text = Localization.Instance.GetString ("notEnoughTitle");
				panel.transform.Find ("tipcosttip").GetComponent<Text> ().text = Localization.Instance.GetString ("notEnoughHit");
        if (SceneManager.GetActiveScene().name == "ShopMenu")
        {
            panel.transform.Find("tipcosttip").GetComponent<Text>().text = "Spin for new Skin cost 120 coins.";
        }
				panel.transform.Find ("btnBuyCoin").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnBuy");
				panel.transform.Find ("btnCancel").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnCancel");

//						if (isVis) {
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

						GameData.getInstance ().lockGame (true);
//						} else {
//						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
//						}
					

		}

		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}

		void OnHideCompleted2(){
				gameObject.SetActive (false);
		}
}
