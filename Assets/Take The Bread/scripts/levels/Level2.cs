using UnityEngine;
using System.Collections;

public class Level2 :MonoBehaviour {
	
	// Use this for initialization
	public GameObject mouse,bread,hole;
	void Start () {
		mouse.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public bool mouseScared =false;
	public bool touched = false;

	

	void failed(){
		GameData.getInstance ().main.gameFailed ();
	}
	
	

		void mouseHitBread(){
				bread.transform.parent = mouse.transform;		
		}



		void tap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if(gesture!=null && gesture.Selection == bread )
				{
						if (touched)
								return;
						touched = true;

						if (!mouseScared) {
								mouse.SetActive (true);
//								mouse.transform.DOMoveX (transform.position.x - 3, 5).OnComplete(failed);
								ATween.MoveTo (mouse, ATween.Hash ("x",mouse.transform.position.x - 5, "time", 5,"easetype","linear", "oncomplete", "failed", "oncompletetarget", this.gameObject));
						} else {
								Destroy (bread);

								GameData.getInstance ().main.gameWin ();		
						}
				}


				if (gesture != null && gesture.Selection == hole) {
						if (!touched) {
								mouseScared = true;
								mouse.SetActive (true);
								mouse.transform.localScale = new Vector3 (-1, 1, 1);
//								mouse.transform.DOMoveX (mouse.transform.position.x + 3, 3);
								ATween.MoveTo (mouse, ATween.Hash ("x",mouse.transform.position.x + 3, "time", 3,"easetype","linear"));
						}
				}
		}




}
