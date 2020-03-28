using UnityEngine;
using System.Collections;

public class Level20 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)//lock user control while win or fail
						return;
				if (bread.transform.position.y < transform.position.y - 1) {//if the bread did not drop into the basket,game fails
						GameData.getInstance().main.gameFailed();		
				}
	}

		public GameObject ankle;//swipe area
		public GameObject bread;
		// spin the yellow cube when swipping it
		void OnSwipe( SwipeGesture gesture )
		{
				// make sure we started the swipe gesture on our swipe object
				GameObject selection = gesture.StartSelection;  // we use the object the swipe started on, instead of the current one

				if( selection == ankle )//start in the swipe area
				{
						bread.GetComponent<HingeJoint2D> ().enabled = false;//break the joint and drop the bread
				}
		}
}
