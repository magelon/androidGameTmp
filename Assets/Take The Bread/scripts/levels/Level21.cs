using UnityEngine;
using System.Collections;

using System.Collections.Generic;
public class Level21 : MonoBehaviour {
	
	// Use this for initialization
	public GameObject pole1,pole2;
		public GameObject saw1,saw2;
		public GameObject bread;
		public GameObject sawDevice;
	void Start () {

	}



		bool canpick =false;
	
	
	// Update is called once per frame
	void FixedUpdate () {
			
		if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)//when fail or win,ingore user control
						return;
				
		if (!canpick) {//bread cant be picked up while the saw have not stoped
			sawDevice.transform.Translate (new Vector3 (0, -.004f, 0));//saw is keep moving down
						if (sawDevice.transform.position.y < bread.transform.position.y) {//if saw is reach a position
								canpick = true;//now can pick up bread
						}
		}

				if (!canpick) {//if you can pick up bread
						saw1.transform.Rotate (new Vector3 (0, 0, -5));	//the saw is keep rotating
						saw2.transform.Rotate (new Vector3 (0, 0, 5));	//the saw is keep rotating
				}

	}

		/// <summary>
		/// Raises the tap event.
		/// </summary>
		/// <param name="gesture">Gesture.</param>
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == bread) {
						if (canpick) {
								bread.SetActive (false);
								GameData.getInstance ().main.gameWin ();
						}
				}
						



		}

		bool Pinching =false;//the whether is pinch flag
		/// <summary>
		/// Raises the pinch event.
		/// </summary>
		/// <param name="gesture">Gesture.</param>
		void OnPinch( PinchGesture gesture )
		{
				
				if( gesture.Phase == ContinuousGesturePhase.Started )
				{
						Pinching = true;//start pinch
				}
				else if( gesture.Phase == ContinuousGesturePhase.Updated )
				{
						if( Pinching )
						{
								//moving the poles and saws by time delta while pinch or pan
								pole1.transform.position+= new Vector3(-Mathf.Sign(gesture.Delta)*.1f,0,0);
								pole2.transform.position+= new Vector3(Mathf.Sign(gesture.Delta)*.1f,0,0);
						}
				}
				else
				{
						if( Pinching )
						{
								Pinching = false;

						}
				}
		}



		void sawed(){
				GameData.getInstance ().main.gameFailed ();
			

				//play a destroy anim
				GameObject tsmoke = GameObject.Find("explode");
				tsmoke.transform.position = bread.transform.position + new Vector3(0,.3f,0);
				tsmoke.GetComponent<Animator> ().SetTrigger ("play");
				Destroy (tsmoke,.6f);

				bread.SetActive (false);
		}
}
