using UnityEngine;

public class WoodObjects : MonoBehaviour
{
    // Float a rigidbody object a set distance above a surface.

    public float floatHeight;     // Desired floating height.
    public float liftForce;       // Force to apply when lifting the rigidbody.
    public float damping;         // Force reduction proportional to speed (reduces bouncing).
    public LayerMask waterLand;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Cast a ray straight down. only with waterlayer
        RaycastHit hit;
            //Physics.Raycast(transform.position, -Vector2.up, waterLandLayer);
        

        // If it hits something...
        if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, waterLand))
        {
            Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            //Debug.Log("hit water");
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;

            // The force is proportional to the height error, but we remove a part of it
            // according to the object's speed.
            float force = liftForce * heightError - rb.velocity.y * damping;

            // Apply the force to the rigidbody.
            rb.AddForce(Vector3.up * force);
        }
    }
}
