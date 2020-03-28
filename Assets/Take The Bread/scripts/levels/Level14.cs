using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Level14 :MonoBehaviour {
	
	// Use this for initialization
		SpriteRenderer idlesp,runsp;
	void Start () {
		StartCoroutine ("wait2");
		onTouching = false;
				runsp = mouse.GetComponent<SpriteRenderer>();
				idlesp = mouse.transform.Find("mouseidle").GetComponent<SpriteRenderer> ();
	}
	public GameObject mouse;
	public GameObject bread;
		public GameObject hole;
		public GameObject[] dragObjects;
		bool started =false;
	IEnumerator wait2(){
		yield return new WaitForSeconds (2);
		mouse.SetActive (true);
				started = true;
	}

	public bool onTouching = false;


		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == hole) {
						
				}


		}
	


	

		bool isCatch =false;
		int holdtime = 0;
		void FixedUpdate () {
				print (onTouching);
				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)//if is win or fail,the game
						return;
				if (!started)
						return;
				if (mouse.transform.position.x > bread.transform.position.x) {
						if(!onTouching || isCatch){//if not touching,the mouse keep close
								mouse.transform.Translate(-.01f,0,0);
								holdtime = 0;
						}
						if(!isCatch){
								idlesp.enabled = onTouching;//if is not touching,the mouse dont idle.
								runsp.enabled = !onTouching;//if is touching,the mouse dont move.
								if(onTouching){//if is touching,count time.
										holdtime++;
										if(holdtime == 200){//touching enough time,the mosue run away,you win.
												started = false;
												GameData.getInstance().main.gameWin();
												idlesp.enabled = false;
												runsp.enabled = true;
												ATween.MoveTo (mouse, ATween.Hash ("x",transform.position.x + 3, "time", 3));
												mouse.transform.localScale = (new Vector3(-1,1,1));
										}
								}else{
										holdtime = 0;//if you released the finger,restart time count.
								}
						}
				}
				if (bread.transform.position.x < -3) {//if the mouse close enough,you fail
						GameData.getInstance().main.gameFailed();		
				}
		}


		void mouseHitBread(){
				isCatch = true;
				bread.transform.parent = mouse.transform;
		}


	

		/// <summary>
		/// Raises the finger hover event.
		/// </summary>
		/// <param name="e">E.</param>
		void OnFingerHover( FingerHoverEvent e )
		{
				if (GameData.getInstance ().isLock)//if locked,ignore all customer interactive
						return;
				if( e.Selection == bread )//if target is bread
				{
						// finger entered the object
						if( e.Phase == FingerHoverPhase.Enter )//start touch
						{
								onTouching = true;//whether touching flag
						}
						else if( e.Phase == FingerHoverPhase.Exit ) // finger left the object
						{
								onTouching = false;//whether touching flag
						}
				}
		}
}
