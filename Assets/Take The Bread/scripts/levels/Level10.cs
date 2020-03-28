using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Level10 :MonoBehaviour {

		// Use this for initialization
		public GameObject[] cups;
		public GameObject bread;
		Vector3[] pos3 = new Vector3[3];
		void Start () {
				StartCoroutine("wait2");

		}

		IEnumerator wait2(){
				yield return new  WaitForSeconds (2);
				int tn = 0;
				foreach (GameObject tcup in cups) {
						//						tcup.transform.DOMoveY(tcup.transform.position.y-.6f,1).OnComplete(playmove);	
						Object[] tparam = { tcup };
						ATween.MoveTo (tcup, ATween.Hash ("ignoretimescale",true,"islocal", false, "y", tcup.transform.position.y-.6f, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "playmove", "oncompleteparams",tparam,"oncompletetarget", this.gameObject));

						pos3[tn] = tcup.transform.position+new Vector3(0,-.6f,0);
						tn++;
				}


		}
		int[] moveround = new int[12];
		int[] desround = new int[12];
		bool ismove = false;
		int breadpos = 1;
		int moveroundNo = 0;

		void playmove(Object[] param){
				if (ismove)
						return;

				bread.transform.parent = cups [1].transform;
				bread.GetComponent<SpriteRenderer> ().enabled = false;
				ismove = true;
				for (int i = 0; i < moveround.Length; i++) {
						moveround[i] = 	(int)Random.Range(0,3);
						int tdes = 	(int)Random.Range(0,3);
						while(tdes == moveround[i]){
								tdes = (int)Random.Range(0,3);
						}
						desround[i] = tdes;
				}



				//				Sequence seq1 = DOTween.Sequence ();
				//				Sequence seq2 = DOTween.Sequence ();
				moveroundNo = 0;

				//				for (int i=0; i< moveround.Length; i++) {
				GameObject tcup = GameObject.Find("cup"+moveround[moveroundNo]);
				GameObject tdescup = GameObject.Find("cup"+desround[moveroundNo]);

				tcup.name = "cup" + desround [moveroundNo];
				tdescup.name = "cup" + moveround [moveroundNo];
				if (moveround [moveroundNo] == breadpos) {
						breadpos = desround [moveroundNo];
				}		


				//						if (i == 0) {
				ATween.MoveTo (tcup, ATween.Hash ("x", pos3 [desround [moveroundNo]].x, "y", pos3 [desround [moveroundNo]].y,"easetype","linear", "time", .4f, "oncomplete", "cupmove1", "oncompletetarget", this.gameObject));
				ATween.MoveTo (tdescup, ATween.Hash ("x", tcup.transform.position.x, "y",tcup.transform.position.y,"easetype","linear", "time", .4f));
				//						}

				//						seq1.Append(tcup.transform.DOMove(pos3[desround[i]],.4f));		
				//						seq2.Append(tdescup.transform.DOMove(pos3[moveround[i]],.4f));

				//				}






				//				seq1.OnComplete (moveover);

		}

		void cupmove1(){
				if (moveroundNo < moveround.Length - 1) {
						GameObject tcup = GameObject.Find ("cup" + moveround [moveroundNo]);
						GameObject tdescup = GameObject.Find ("cup" + desround [moveroundNo]);

						tcup.name = "cup" + desround [moveroundNo];
						tdescup.name = "cup" + moveround [moveroundNo];
						if (moveround [moveroundNo] == breadpos) {
								breadpos = desround [moveroundNo];
						}


						ATween.MoveTo (tcup, ATween.Hash ("x", pos3 [desround [moveroundNo]].x, "y", pos3 [desround [moveroundNo]].y, "time", .4f, "oncomplete", "cupmove1", "easetype", "linear", "oncompletetarget", this.gameObject));
						ATween.MoveTo (tdescup, ATween.Hash ("x", tcup.transform.position.x, "y", tcup.transform.position.y, "easetype", "linear", "time", .4f));
						moveroundNo++;


				} else {
						moveover ();
				}

		}


		void moveover(){

				cantouch = true;

		}

		// Update is called once per frame
		void Update () {

		}


		bool isshowedup =false;


		public void gameover(GameObject showcup){
				bread.GetComponent<SpriteRenderer> ().enabled = true;
				if (showcup.transform.Find ("bread") != null) {

						GameData.getInstance ().main.gameWin ();


				} else {
						GameData.getInstance ().main.gameFailed ();	
				}
				bread.transform.parent = null;
				int tn = 0;
				foreach (GameObject tcup in cups) {
						//						tcup.transform.DOMoveY(tcup.transform.position.y+.6f,1);
						ATween.MoveTo (tcup, ATween.Hash ("y",tcup.transform.position.y+.6f, "time", 1));

						tn++;
				}
				isshowedup = true;
		}


		bool cantouch = false;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null) {
						for (int i = 0;i<cups.Length;i++) {
								if (gesture.Selection == cups [i]) {
										if (cantouch) {
												gameover (cups [i]);
												cantouch = false;
										}
								}
						}

				}
		}



}
