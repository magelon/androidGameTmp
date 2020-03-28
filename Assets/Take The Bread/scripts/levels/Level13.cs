using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Level13 :MonoBehaviour {

		// Use this for initialization
		public GameObject bread,fragile,stone,wallhole,breadfake,destroywall;
		void Start () {


		}


		int n = 0;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == destroywall) {
						n++;
						if (n == 12) { 
								wallhole.SetActive(true);
								bread.SetActive(true);
								Destroy(destroywall);
								GameManager.getInstance().playSfx("break");
						}
				}



				if (gesture != null && gesture.Selection == breadfake) {
//						DestroyObject (bread);
						breadfake.SetActive (false);
						stone.SetActive(true);
						GameData.getInstance ().main.gameFailed ();
				} 
				if (gesture != null && gesture.Selection == bread) {
            Object.Destroy(bread);
            //DestroyObject (bread);
						GameData.getInstance ().main.gameWin ();
				}
		}

}
