using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class PanelAskTip : MonoBehaviour {
	
	// Use this for initialization


	public Transform panel;
		public GameObject panelNotEnough;
		public GameObject panelDisplayTip;
		public GameObject panelBuyCoin;
	void Start () {
				

		
	}
	
	public void showMe(){
				initView ();
			


				if (GameData.getInstance ().coin >= 20 ) {
						
						panelNotEnough.SetActive (false);
						if (!GameData.getInstance ().cLvShowedTip) {
								ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 40, "time", .7f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));
						} else {
								ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
								panelDisplayTip.SetActive (true);
								panelDisplayTip.GetComponent<PanelDisplayTip> ().showMe ();
								gameObject.SetActive (false);
						}
						//disable some UI;

				} else {

						panelNotEnough.SetActive (true);
//						GameObject panelNotEnough_ = panelNotEnough.transform.Find ("panel").gameObject;
						panelNotEnough.GetComponent<PanelNotEnough> ().showMe ();
//						ATween.MoveTo (panelNotEnough_, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));
						gameObject.SetActive (false);

				}
				GameData.getInstance ().lockGame (true);
	}

		void initView(){
				panel.transform.Find ("tiptitle").GetComponent<Text> ().text = Localization.Instance.GetString ("askTipTitle");
				panel.transform.Find ("tipcosttip").GetComponent<Text> ().text = Localization.Instance.GetString ("askTipHit");
				panel.transform.Find ("btnYes").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnYes");
				panel.transform.Find ("btnCancel").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnCancel");
		}
	
	// Update is called once per frame
	void Update () {
				
	}
	
	bool locker;
		public void OnClick(GameObject g )
	{
		// Add event handler code here
		switch (g.name) {
				case "btnCancel":
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
						break;
				case "btnYes":
						GameData.getInstance ().coin -= 20;
						PlayerPrefs.SetInt ("coin", GameData.getInstance ().coin);
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
						GameData.getInstance ().main.txtCoin.text = GameData.getInstance ().coin.ToString ();
						panelDisplayTip.SetActive (true);
						panelDisplayTip.GetComponent<PanelDisplayTip> ().showMe ();
						break;
		
		}
		
	}
	
	
		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}
	
	
	
}
