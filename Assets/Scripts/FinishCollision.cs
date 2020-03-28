using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stop the object drop
public class FinishCollision : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<Rigidbody>().isKinematic = true;
    }
    
}
