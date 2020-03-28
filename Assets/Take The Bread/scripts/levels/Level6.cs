using UnityEngine;
using System.Collections;
//using DG.Tweening;
using System.Collections.Generic;


public class Level6 :MonoBehaviour {

		// Use this for initialization
		int startn;
		void Start () {


		}



		// Update is called once per frame
		void Update () {

		}


		public GameObject explodeAnim;
		public GameObject bread;
		public GameObject timeBomb;
		public GameObject hourNeedle;
		public GameObject minuteNeedle;



		int cHour = 12;
		bool islock;//lock when rotating;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == bread) {
						if (cHour == 6) {// level6
								bread.SetActive(false);
								GameData.getInstance ().main.gameWin ();
						} else {
								GameObject explodeanim = Instantiate(explodeAnim,timeBomb.gameObject.transform.position,Quaternion.identity) as GameObject;
								Destroy(explodeanim,2);
								GameData.getInstance ().main.gameFailed ();
								Destroy(timeBomb.gameObject);
								bread.SetActive(false);
								GameManager.getInstance().playSfx("explosion");
						}

				}

				if (gesture != null && gesture.Selection == timeBomb) {
						if (islock)
								return;
						if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
								return;


						ATween.RotateTo (minuteNeedle, ATween.Hash ("islocal",true,"z",-180, "time", .5, "oncomplete", "m1over","easetype","linear", "oncompletetarget", this.gameObject));
						islock = true;
				}
		}

		void m1over(){
				ATween.RotateTo (minuteNeedle, ATween.Hash ("islocal",true,"z",-360, "time", .5, "oncomplete", "moveh","easetype","linear", "oncompletetarget", this.gameObject));
		}



		void moveh(){
				cHour++;

				ATween.RotateTo (hourNeedle, ATween.Hash ("islocal",false,"z",-360/12*cHour, "time", .5));
				if (cHour == 13)
						cHour = 1;
				islock = false;
		}
}
