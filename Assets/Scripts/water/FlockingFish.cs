using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//globalFlock
public class FlockingFish : MonoBehaviour {
    public FlockingFish myFlock;
    public GameObject fishPrefab;
    static int numFish = 10;
   
    public static GameObject[] allFish = new GameObject[numFish];
    public static Vector3 goalPos;
    public Vector3 swimLimits = new Vector3(5, 5, 5);

    public void FishSpeed(float speedMult)
    {
        Debug.Log(speedMult);
        for(int i = 0; i < numFish; i++)
        {
            allFish[i].GetComponent<flock>().speedMult = speedMult;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position,new Vector3(swimLimits.x*2,
        swimLimits.y*2,swimLimits.z*2));
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawSphere(goalPos, 0.1f);
    }

    // Use this for initialization
    void Start () {
        myFlock = this;
        goalPos = this.transform.position;
        /*camera fog
         * renderSetting.fogColor=Camera.main.backgroundColor;
         * renderSetting.fogDensity=0.03f;
         * RenderSetting.fog=true;**/        
        //goalPos = transform.position;
        for (int i = 0; i < numFish; i++) {
            Vector3 pos = new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                        Random.Range(-swimLimits.y, swimLimits.y),
                                        Random.Range(-swimLimits.z, swimLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 10000) < 50) {
            goalPos = new Vector3(Random.Range(-swimLimits.x+goalPos.x, swimLimits.x+goalPos.x),
                Random.Range(-swimLimits.y+goalPos.y, swimLimits.y+goalPos.y),
                Random.Range(-swimLimits.z+goalPos.z, swimLimits.z+goalPos.z));
        }
	}
}
