using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Level19 : MonoBehaviour {

		// Use this for initialization
		public GameObject bread1,bread2;
		void Start () {

		}

		// Update is called once per frame









		int npick = 0;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == bread1) {
				}		
				if (gesture != null && gesture.Selection == bread2) {

				}
		}

		/// <summary>
		/// Raises the finger hover event.
		/// </summary>
		/// <param name="e">E.</param>
		void OnFingerHover( FingerHoverEvent e )
		{
				if (GameData.getInstance ().isLock)//if game locked,you can not control the game
						return;
				if( e.Selection == bread1 )//deal with the event while bread1 was touched
				{
						// finger entered the object
						if( e.Phase == FingerHoverPhase.Enter )
						{
								//this level simulate a mutilple touch at same time.When system detect one touch,we start a timer,check whether another touch happenes in 0.1 second.


								StartCoroutine ("bread2disappear");		
								npick++;//plus picked number
								bread1.SetActive (false);	//pick up bread1								
								if (npick == 2) {
										GameData.getInstance ().main.gameWin ();//detect 2 bread picked.Win the game
										StopAllCoroutines ();//stop time check
								}

						}

				}


				if( e.Selection == bread2 )//deal with the event while bread2 was touched
				{
						// finger entered the object
						if( e.Phase == FingerHoverPhase.Enter )
						{
								//this level simulate a mutilple touch at same time.When system detect one touch,we start a timer,check whether another touch happenes in 0.1 second.
								StartCoroutine ("bread1disappear");	
								npick++;//plus picked number
								bread2.SetActive (false);//pick up bread2

								if (npick == 2) {
										GameData.getInstance ().main.gameWin ();//detect 2 bread picked.Win the game
										StopAllCoroutines ();//stop time check
								}
						}
				}

		}


		IEnumerator bread1disappear(){
				yield return new WaitForSeconds (.1f);
				//player did not picked another bread quick enough
				bread1.SetActive (false);
				GameData.getInstance ().main.gameFailed ();
		}
		IEnumerator bread2disappear(){
				yield return new WaitForSeconds (.1f);
				//player did not picked another bread quick enough
				bread2.SetActive (false);
				GameData.getInstance ().main.gameFailed ();

		}
}
