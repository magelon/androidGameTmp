using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light dlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dlight.intensity = transform.position.y*2;
        transform.RotateAround(Vector3.zero,Vector3.right,10*Time.deltaTime);
        transform.LookAt(Camera.main.transform);
    }
}
