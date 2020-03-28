using UnityEngine;
using System.Collections;

public class Level17 :MonoBehaviour {

	// Use this for initialization
	public GameObject[] breads;
		public GameObject black;
	void Start () {
				StartCoroutine("wait3");
				isblack = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}



		bool isblack =false;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				foreach (GameObject bread in breads) {
						if (gesture != null && gesture.Selection == bread) {
								if(isblack)
										DestroyImmediate (bread);
						}
				}
		}



		IEnumerator wait3(){
				yield return new WaitForSeconds (3);
				isblack = true;
				black.SetActive (true);
				StartCoroutine("wait32");
		}

		IEnumerator wait32(){
				yield return new WaitForSeconds (3);
				isblack = false;
				black.SetActive (false);
				GameObject bread = GameObject.Find("bread");
				if (bread == null) {
						GameData.getInstance ().main.gameWin ();		
				} else {
						GameData.getInstance ().main.gameFailed ();			
				}

		}
}
