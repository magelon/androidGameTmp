using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelAskSkip : MonoBehaviour {

	// Use this for initialization
//	dfLabel lb_tipLeft,lb_skipins;


		public Transform panel;
		public GameObject panelNotEnough;
		public GameObject panelDisplayTip;
		public GameObject panelBuyCoin;

        public GameObject gachaButton;
        
    void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void showMe(){
				initView ();

				if (GameData.getInstance ().coin >= 60) {

						panelNotEnough.SetActive (false);
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

						//disable some UI;

				} else {
                     if (gachaButton)
                        {
                        gachaButton.SetActive(false);
                        }
          
						panelNotEnough.SetActive (true);

						panelNotEnough.GetComponent<PanelNotEnough> ().showMe ();
					    
						gameObject.SetActive (false);

				}
				GameData.getInstance ().lockGame (true);
	}

  

    void dispalySpinResult()
    {
        panelDisplayTip.SetActive(true);
        panelDisplayTip.GetComponent<PanelDisplayTip>().showMe();
        //disable spin button
        gachaButton.SetActive(false);
    }

		public void OnClick(GameObject g)
	{

        // Add event handler code here
        switch (g.name)
        {
            case "btnCancel":
                ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));

                break;
            case "btnYes":
                if (SceneManager.GetActiveScene().name == "ShopMenu" && GameData.getInstance().coin >= 120)
                {
                    GameData.getInstance().coin -= 120;
                    PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                    ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                    GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                    //gacha animation
                    if (gachaButton)
                    {
                        gachaButton.GetComponent<Animator>().SetTrigger("spin");
                        GameManager.getInstance().playSfx("win");
                        //get result from it
                        Invoke("dispalySpinResult", 1f);
                    }

                }
                else
                {
                    if (gachaButton)
                    {
                        gachaButton.SetActive(false);
                    }

                    panelNotEnough.SetActive(true);

                    panelNotEnough.GetComponent<PanelNotEnough>().showMe();

                    gameObject.SetActive(false);

                }
                if (SceneManager.GetActiveScene().name != "ShopMenu") { 
                GameData.getInstance().coin -= 60;
                PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
                ATween.MoveTo(panel.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
                GameData.getInstance().main.txtCoin.text = GameData.getInstance().coin.ToString();
                GameData.getInstance().main.nextLevelSkip();
                }
                    break;

		}
		
	}
		void initView(){

        if (SceneManager.GetActiveScene().name == "ShopMenu")
        {
            panel.transform.Find("skiptitle").GetComponent<Text>().text = "Spin to Win new Skins!";
            panel.transform.Find("skiptip").GetComponent<Text>().text = "One Spin cost 120";
        }
        else
        {

        
				panel.transform.Find ("skiptitle").GetComponent<Text> ().text = Localization.Instance.GetString ("askSkipTitle");
				panel.transform.Find ("skiptip").GetComponent<Text> ().text = Localization.Instance.GetString ("askSkipHit");
				panel.transform.Find ("btnYes").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnYes");
				panel.transform.Find ("btnCancel").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnCancel");
        }
    }


		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}
}
