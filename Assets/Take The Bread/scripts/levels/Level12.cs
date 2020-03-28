using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Level12 :MonoBehaviour {

		// Use this for initialization

		void Start () {
				sp = doorMark.GetComponent<SpriteRenderer> ();

		}
		public GameObject[] dragObjects;
		public GameObject bread;

		void FixedUpdate () {
				sp.color = new Color (1, 1, 1, sp.color.a - .003f);

		}
		Vector3 starttouchpos;
		SpriteRenderer sp;
		void OnDrag( DragGesture gesture )
		{

				if (GameData.getInstance ().isLock)
						return;
				if( gesture.Phase == ContinuousGesturePhase.Started )
				{
						foreach (GameObject dragObject in dragObjects) {
								if (gesture.Selection == dragObject) {
								if(dragObject == dragObjects[0]){//door
												starttouchpos = dragObject.transform.position;
								}
								}
						}

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

		bool isopened = false;
		public GameObject doorMark;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isWin || GameData.getInstance ().isFail)
						return;
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == bread) {
                        Object.Destroy(bread);
						//DestroyObject (bread);
						GameData.getInstance ().main.gameWin ();
				}



				GameObject tdoor = dragObjects[0];
			
				if (gesture != null && gesture.Selection == tdoor) {
//						if(gesture.Selection.gameObject.transform.position == starttouchpos){
								//have not moved;

								GameObject door1 = tdoor.transform.Find ("door1").gameObject;
								GameObject door2 = tdoor.transform.Find ("door2").gameObject;
								door1.SetActive(false);
								door2.SetActive(true);
								if(!isopened){
										GameManager.getInstance().playSfx("kata");
										isopened = true;

								if(Mathf.Abs(tdoor.transform.position.x - doorMark.transform.position.x) < .1f){
												if(sp.color.a < .01f){
														GameData.getInstance().main.gameFailed();
												}else{
												bread.transform.position = tdoor.transform.position+new Vector3(0,-.9f,-1);
												}
										}else{
												GameData.getInstance().main.gameFailed();
										}
								}
//						}
				}
		}

}
