using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Trigger : MonoBehaviour {

		// Use this for initialization
		GameObject g;
		void Start () {
				g = GameObject.Find ("gameContainer");
		}

		// Update is called once per frame
		void Update () {

		}

		/// <summary>
		/// Raises the trigger enter event.
		/// If you need something to be triggered add a trigger script to it and write its event below for that level
		/// </summary>
		/// <param name="collider">Collider.</param>
		void OnTriggerEnter(Collider collider){
				switch(SceneManager.GetActiveScene().name){
				case "level2":
						if (collider.name == "mouse") {
								g.SendMessage ("mouseHitBread");
						}
						break;
				case "level4":
						if (collider.tag == "game") {
								Destroy (collider.gameObject);
								GameData.getInstance ().main.gameFailed ();
						} else if (collider.name == "bread") {
								collider.gameObject.SetActive (false);
								GameData.getInstance ().main.gameWin ();
						}
						break;
				case "level9":
						if (collider.name == "bread") {
								if (Mathf.Abs(collider.transform.position.y - transform.position.y) < 1) {
										collider.GetComponent<Rigidbody> ().isKinematic = true;
										collider.transform.parent = transform;
										collider.transform.position = -new Vector3 (.2f, .8f, 0);
										GameData.getInstance ().main.gameWin ();	
								}
						}
						break;
				case "level11":
						if (collider.name == "bread") {
								GameData.getInstance ().main.gameWin ();
								collider.gameObject.SetActive (false);
						}
						break;
				case  "level14":
						if (collider.name == "bread") {
								g.SendMessage ("mouseHitBread");
						}
						break;

				case "level16":
						if (collider.name == "hole") {
								g.SendMessage ("dropInHole");
						} else if (collider.name == "holecover") {
								g.SendMessage ("dropOnCover");
						}
						break;
				case "level20":
						if (collider.name == "breadcollider") {
								GameData.getInstance ().main.gameWin ();
								collider.transform.parent.gameObject.SetActive (false);
						}
						break;
				case "level21":
						if (collider.name == "lopen") {
								g.SendMessage ("sawed");
						} else if (collider.name == "ropen") {
								g.SendMessage ("sawed");
						}
						break;
				}
		}




}
