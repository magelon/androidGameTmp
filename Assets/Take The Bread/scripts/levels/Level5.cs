using UnityEngine;
using System.Collections;
//using DG.Tweening;
using System.Collections.Generic;
public class Level5 :MonoBehaviour {
	
		public GameObject bread;
	// Use this for initialization
	int startn;
	void Start () {
		startn = (int)Random.Range (1, 4);
		StartCoroutine ("startThrow");
		
	}

	IEnumerator startThrow(){
		yield return new WaitForSeconds (startn);
		bread.GetComponent<Rigidbody>().isKinematic = false;
		bread.GetComponent<Rigidbody>().AddForce (200, 400, 0);
	}

	// Update is called once per frame
	void Update () {
		if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
			return;
		if (bread.transform.position.y <-5) {
			GameData.getInstance().main.gameFailed();		
		}
	}
	
	
//	public override void touchBegin (Gesture gesture)
//	{
//
//		base.touchBegin (gesture);
//		gameObject.SetActive (false);
//
//		GameData.getInstance ().main.gameWin ();
//
//	}

		void OnTap( TapGesture gesture )
		{
			
				if (GameData.getInstance ().isLock)
						return;

				if (gesture != null && gesture.Selection == bread) {
						
						bread.SetActive (false);
				
						GameData.getInstance ().main.gameWin ();	
				}
		}
	
	
	
	
	
	
}
