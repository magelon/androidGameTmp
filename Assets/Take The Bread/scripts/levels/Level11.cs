using UnityEngine;
using System.Collections;

public class Level11 : MonoBehaviour {

		public GameObject[] dragObjects;
		public GameObject bread;

	// Use this for initialization
	void Start () {
	
	}
	


		void OnDrag( DragGesture gesture )
		{

				if (GameData.getInstance ().isLock)
						return;
				if( gesture.Phase == ContinuousGesturePhase.Started )
				{

				}
				else if( gesture.Phase == ContinuousGesturePhase.Updated )
				{

						foreach (GameObject dragObject in dragObjects) {
								if (gesture.Selection == dragObject) {

										dragObject.transform.position = Util.GetWorldPos (gesture.Position,dragObject,true);

								}
						}

				}
				else
				{
						foreach (GameObject dragObject in dragObjects) {
								if (gesture.Selection == dragObject) {
									
								}
						}

				}

		}

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
				bread.transform.Translate (new Vector3(dir.x * speed,0,0));
				bread.transform.Rotate (new Vector3(0,0,dir.x));
		}
}
