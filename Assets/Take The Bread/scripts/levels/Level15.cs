using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Level15 :MonoBehaviour {


		// Use this for initialization
		public GameObject bread;
		void Start () {


		}
		int timecount = 0;
		void FixedUpdate(){
				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
						return;
				if (GameData.getInstance ().isLock)
						return;
				if (n == 11) {
						timecount++;
						if (timecount == 200) {
								GameData.getInstance ().main.gameWin ();
						}
				} else {
						timecount = 0;
				}
		}

		public static bool onTouching = false;
//		List<SpriteSlicer2DSliceInfo> m_SlicedSpriteInfo = new List<SpriteSlicer2DSliceInfo>();
		int n = 1;


		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == bread) {
						bread.transform.localScale += new Vector3 (n / 100f,  n / 100f, 1f);
						n ++;
						timecount = 0;
						GameManager.getInstance().playSfx("select");
						if (n == 12) {

				
								bread.SetActive (false);
//								
								//play a destroy anim
								GameObject tsmoke = GameObject.Find("explode");
								tsmoke.transform.position = bread.transform.position + new Vector3(0,.3f,0);
								tsmoke.GetComponent<Animator> ().SetTrigger ("play");
								Destroy (tsmoke,.6f);

								GameData.getInstance().main.gameFailed();	
						}
				}
		}


}
