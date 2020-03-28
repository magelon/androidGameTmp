using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
public class PanelNoTip : MonoBehaviour {
	
	// Use this for initialization
	string nextTipStr = "";
	void Start () {

		

	}
	
	bool canTick = true;
	int n = 0;
	void Update () {
		if (n < 20) {
			n++;
			return;
		} else {
			n= 0;		
		}
		if (!canTick)
			return;
		if (GameData.getInstance ().tipRemain == 0) {
			
			
			TimeSpan ts=new TimeSpan(50,0,0,0);  
			DateTime dt2=DateTime.Now.Subtract(ts); 
			long cTime = dt2.Ticks/10000000;
			
			
			
			long tTimeLasts = (long)(cTime - long.Parse(GameData.getInstance().tickStartTime));
			//			print (cTime+"_________"+"starttime"+GameData.getInstance().tickStartTime+"_____"+tTimeLasts);
			
			long secondRemain = 180 - tTimeLasts;
			//			print(secondRemain);
			if (secondRemain <= 0) {
				secondRemain = 0;
				//count of;
				PlayerPrefs.SetInt ("tipRemain", 1);
				//				PlayerPrefs.SetString ("tipStart", "0");
				GameData.getInstance ().tipRemain = 1;
				//				GameData.getInstance ().tickStartTime = "0";
//				GameData.getInstance().main.refreshView();
//				transform.GetComponent<dfPanel>().IsVisible = false;
				
			}
			//			print (second);
//			lb_tipLeft.Text = nextTipStr + secondRemain.ToString();		
			
		}
	}
	
	bool locker;
//	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
//	{
//		// Add event handler code here
//		switch (control.name) {
//		case "btnNoTipYes":
//			if(!locker){
//				GameManager.getInstance ().playSfx ("click");
//				GameData.getInstance().main.fadeInOnly.Length = 1;
//				GameData.getInstance().main.fadeIn();
//				locker = true;
//				StartCoroutine("showStore");
//			}
//			GameData.getInstance().isLock = false;
//			break;
//		case "btnNoTipNo":
//			GameManager.getInstance ().playSfx ("click");
//			gameObject.GetComponent<dfPanel>().IsVisible = false;
//			GameData.getInstance().isLock = false;
//			break;
//		}
//	}
	
	IEnumerator showStore(){
		yield return new WaitForSeconds(1);
        SceneManager.LoadScene("store");
		//Application.LoadLevel("store");
	}

	public void showMe(bool isVis){
//		GetComponent<dfPanel>().IsVisible = isVis;
//		//		lb_tipLeft.Text =  LanguageManager.Instance.GetTextValue("GAME_TIPLEFT")+" " +GameData.getInstance().tipRemain;

		if (isVis) {
						GameData.getInstance ().lockGame (true);	
		}
	}
}
