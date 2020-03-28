using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundOnCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        if (collision.relativeVelocity.magnitude > 2)
        {
            GameManager.getInstance().playSfx("select");
        }
        //audioSource.Play();
    }

}