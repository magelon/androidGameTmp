using UnityEngine;
using System.Collections;
//using DG.Tweening;
public class Level9  :MonoBehaviour {
	
	// Use this for initialization
	bool isdrop =false;
	void Start () {
		StartCoroutine("wait3");
	}
	
	IEnumerator wait3(){
		yield return new WaitForSeconds (3);
		bread.GetComponent<Rigidbody>().isKinematic = false;
		bread.GetComponent<Rigidbody>().AddForce (new Vector3(0,-200,0));
		isdrop = true;
	}
	
	IEnumerator wait1(){
		yield return new WaitForSeconds (1);
		if (!isdrop) {
			
			bread.GetComponent<Rigidbody>().isKinematic = false;
			bread.GetComponent<Rigidbody>().AddForce (new Vector3 (0, -200, 0));
		}
	}
	// Update is called once per frame
	void Update () {
				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
						return;
				if (bread.transform.position.y < - 5) {
						GameData.getInstance().main.gameFailed();		
				}
	}
	public GameObject hand, bread;
		public GameObject touchArea;
	bool locker = false;
	
	void over(){
		locker = true;
	}


		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == touchArea) {
						if(locker)return;
						locker = true;
//						Sequence seq = DOTween.Sequence ();
//						seq.Append(hand.transform.DOMoveX (hand.transform.position.x - 2,.2f,false));
//						seq.Append(hand.transform.DOMoveX(hand.transform.position.x,1f,false).OnComplete(over));
//						if (!isdrop) {
//								//			StopCoroutine("wait3");
//								StartCoroutine("wait1");
//						}
						//test fucking idiot
						ATween.MoveTo (hand, ATween.Hash ("x", hand.transform.position.x - 2, "time", .2f, "oncomplete", "handout", "oncompletetarget", this.gameObject));

				}
		}

		void handout(){
				ATween.MoveTo (hand, ATween.Hash ("x", hand.transform.position.x+2 , "time", 1f, "oncomplete", "over", "oncompletetarget", this.gameObject));
		}
}
