using UnityEngine;
using System.Collections;
//using DG.Tweening;
using System.Collections.Generic;

public class Level4 :MonoBehaviour {
	
	// Use this for initialization
	public GameObject[] food;
	public GameObject bread;
	public GameObject[] dragObjects;
	int endn = 0;
	void Start () {
		StartCoroutine("tick");
		endn = (int)Random.Range (5, 12);
	}
	bool istick =true;
	IEnumerator tick(){
		while(istick){
			yield return new WaitForSeconds(2);
			if(endn <= 0){
				bread.GetComponent<Rigidbody>().isKinematic = false;
				bread.GetComponent<Rigidbody>().AddForce(Vector3.up);
				istick = false;
			}else{

				int trnd = (int)Random.Range(0,5);
				float trndx = Random.Range(-1.5f,1.5f);
				GameObject tfood = Instantiate(food[trnd],new Vector3(trndx,3,0),Quaternion.identity) as GameObject;
				tfood.GetComponent<Rigidbody>().isKinematic =false;
				Destroy(tfood,3);
				endn--;
			}

		}

	}
	// Update is called once per frame
	void Update () {
				if (GameData.getInstance ().isFail || GameData.getInstance ().isWin)
						return;
				if (bread.transform.position.y < -5) {
						GameData.getInstance().main.gameFailed();		
				}
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
										//released										

//										if (dragObject == steelstick) {
//												float tdis = Vector2.Distance (trapoff.transform.position, steelstick.transform.position);
//												if (tdis < .4f && !touched) {
//														DestroyImmediate (dragObject);
//														steelfixed.SetActive (true);
//												} else {
//														steelstick.transform.position = steelStartPos;
//												}
//										}
								}
						}

				}

		}



}
