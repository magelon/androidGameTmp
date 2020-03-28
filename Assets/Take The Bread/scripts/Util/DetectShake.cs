using UnityEngine;
using System.Collections;

public class DetectShake : MonoBehaviour {
	float accelerometerUpdateInterval   = 1.0f / 60.0f;
	float lowPassKernelWidthInSeconds  = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
	float shakeDetectionThreshold  = 2.0f;
	
	private float lowPassFilterFactor; 
	private Vector3 lowPassValue   = Vector3.zero;
	private Vector3 acceleration ;
	private Vector3 deltaAcceleration ;
	

	void Start()
	{
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}
	
	
	void FixedUpdate()
	{
		acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		deltaAcceleration = acceleration - lowPassValue;
		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
		{
						transform.SendMessage ("shake");
		}
	}
}
