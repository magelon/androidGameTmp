using UnityEngine;
using System.Collections;

public class Level16 : MonoBehaviour {
	
	// Use this for initialization
	public GameObject holeCover,hole,bread;
	void Start () {
		StartCoroutine ("wait3");
	}

	IEnumerator wait3(){
		yield return new WaitForSeconds (5);
		bread.GetComponent<Rigidbody>().isKinematic = false;
				bread.GetComponent<Rigidbody>().AddForce (Vector3.down);
	}
	
	bool canpick = false;
	


	
		float speed = 10.0f;
		void Update () {
				//if (!GameData.getInstance ().startgame)
				//return;

				Vector3 dir  = Vector3.zero;

				// we assume that device is held parallel to the ground
				// and Home button is in the right hand

				// remap device acceleration axis to game coordinates:
				//  1) XY plane of the device is mapped onto XZ plane
				//  2) rotated 90 degrees around Y axis
				dir.x = Input.acceleration.x;
				if(Mathf.Abs(dir.x)<=.1f)return;
				//		dir.z = Input.acceleration.x;

				// clamp acceleration vector to unit sphere
				if (dir.sqrMagnitude > 1)
						dir.Normalize();

				// Make it move 10 meters per second instead of 10 meters per frame...
				dir *= Time.deltaTime;

				// Move object
				holeCover.transform.Translate (new Vector3(dir.x * speed,0,0));
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
								DestroyObject(bread);
								GameData.getInstance().main.gameWin();
						}
				}
		}

		/// <summary>
		/// detect which object did the bread hit
		/// </summary>
		void Coverd(){
				if (GetComponent<Collider>().name == "hole") {//if the bread hit the hole,you fail the level
						if(!GetComponent<Rigidbody>().isKinematic){
								gameObject.SetActive (false);	
								GameData.getInstance().main.gameFailed();
						}
				} else if (GetComponent<Collider>().name == "holecover") {//if the bread hit the cover.It wont drop into the hole and you win the level.
						GetComponent<SphereCollider>().isTrigger = false;	
						GetComponent<Rigidbody>().isKinematic = true;
						transform.position = new Vector3(transform.position.x, GetComponent<Collider>().transform.position.y+.2f,0);
						canpick = true;
				}
		}

		/// <summary>
		/// Drops the in hole.
		/// </summary>
		void dropInHole(){
				if(!bread.GetComponent<Rigidbody>().isKinematic){
						bread.gameObject.SetActive (false);	
						GameData.getInstance().main.gameFailed();
				}
		}
		/// <summary>
		/// Drops the on cover.
		/// </summary>
		void dropOnCover(){
				bread.GetComponent<SphereCollider>().isTrigger = false;	
				bread.GetComponent<Rigidbody>().isKinematic = true;
				bread.transform.position = new Vector3(holeCover.transform.position.x, bread.transform.position.y+.2f,0);
				canpick = true;
		}

}
