using UnityEngine;
using System.Collections;

public class Level8 : MonoBehaviour {

		public GameObject bread;
		// Use this for initialization
		void Start () {
//				shake ();
		}

		// Update is called once per frame
		void Update () {

		}



		/// <summary>
		/// Shake the device
		/// </summary>
		void shake(){
				//shake screen effect
				ATween.ShakePosition(Camera.main.transform.gameObject, 
						ATween.Hash("x",1, "time", 1, "delay",0,"easetype", ATween.EaseType.easeInOutQuint));

				//active the hidden bread's physical and drop it off
				bread.GetComponent<Rigidbody>().isKinematic = false;
				bread.GetComponent<Rigidbody>().AddForce(Vector3.down);
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
						DestroyObject (bread);
						GameData.getInstance ().main.gameWin ();
				}
		}
}
