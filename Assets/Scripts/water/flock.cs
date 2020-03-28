using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//source https://www.youtube.com/watch?v=eMpI1eCsIyM
public class flock : MonoBehaviour {

    public FlockingFish myManager;
    public float speed = 0.001f;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 3.0f;
    bool turning = false;

    public float speedMult = 1;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.5f, 1);
	}
	
	// Update is called once per frame
	void Update () {

        Bounds b = new Bounds(myManager.transform.position,
        myManager.swimLimits*2);

        if (!b.Contains(transform.position))
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            Vector3 direction = myManager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(direction),
                rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1)*speedMult;
        }
        else
        {
            if (Random.Range(0, 5) < 1)
                ApplyRules(); 
        }

        transform.Translate(0, 0, Time.deltaTime * speed*speedMult);
    }

    void ApplyRules() {
        GameObject[] gos;
        gos = FlockingFish.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = FlockingFish.goalPos;
        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos) {
            if (go != this.gameObject) {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance) {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f) {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    flock anotherFlock = go.GetComponent<flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize*speedMult;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                    rotationSpeed * Time.deltaTime);

        }
    }
}
