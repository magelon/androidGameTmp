using UnityEngine;
using System.Collections;
//using DG.Tweening;
using System.Collections.Generic;

public class Level7 :MonoBehaviour {
	
	// Use this for initialization
	void Start () {

				anim = girl.GetComponent<Animator> ();
				anim2 = girl.transform.Find ("girlwalk").GetComponent<Animator> ();
	}




		public GameObject girl;
		public GameObject bread;
		public GameObject touchArea;
		Animator anim,anim2;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
						return;
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == bread) {
						if (walkround <= 0) {
                Object.Destroy(bread); //DestroyObject (bread);
								GameData.getInstance ().main.gameWin ();
						}
				}






				if (gesture != null && gesture.Selection == touchArea) {
						if (bread.GetComponent<Rigidbody> ().isKinematic) {
								bread.GetComponent<Rigidbody> ().isKinematic = false;
								bread.GetComponent<Rigidbody> ().AddForce (Vector3.down);
                                Object.Destroy(touchArea);//DestroyObject (touchArea);
						}
				}
		}

		float xspeed = -.03f;
		bool stop = false;
		int walkround = 4;
		void FixedUpdate () {
				if (stop)
						return;
				if (girl.transform.position.x + xspeed > 4 || girl.transform.position.x + xspeed < -4) {
						xspeed*=-1;	
						//			transform.localScale = new Vector3(-1*Mathf.Sign(xspeed),1,1);

						girl.transform.GetComponent<SpriteRenderer> ().flipX = xspeed > 0;
						girl.transform.Find("girlwalk").GetComponent<SpriteRenderer>().flipX =  xspeed > 0;

						//			anim2.gameObject.transform.localScale =  new Vector3(-0.83f*Mathf.Sign(xspeed),0.83f,1);
						walkround--;
				}
				if(walkround > 0)
						girl.transform.Translate (xspeed,0,0);
				if (!bread)
						return;
				if (Mathf.Abs (girl.transform.position.x - bread.transform.position.x) < .2f && bread.transform.position.y < girl.transform.position.y) {
						if(anim)anim.SetTrigger("pickup");
						if(anim2)anim2.SetTrigger("pickup");
						stop = true;
						StartCoroutine("wait1second");
                        Object.Destroy(bread, 0.5f);
						//DestroyObject(bread,.5f);
						GameData.getInstance().main.gameFailed();
				}

		}
	
		IEnumerator wait1second(){
				yield return new WaitForSeconds (1);
				stop = false;
		}

}
