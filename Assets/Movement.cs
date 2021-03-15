using System.Collections;
using System.Collections.Generic;
using UnityEngine; // namespace where MonoBehavior data lives

public class Movement : MonoBehaviour
{
    // Member variable (available throughout entire class)
    Rigidbody rb;
    [SerializeField] float thrustForce;
    [SerializeField] float rotationThrust;

    void Start() {
        // Caches a reference to our component
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        // Use KeyCode.name signature since (string name) sig can be tricky to get correct
        if (Input.GetKey(KeyCode.Space)) { 
            // //rb.AddRelativeForce(Vector3.up) is the same as the following line
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        } 
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D)) {
            ApplyRotation(-rotationThrust);
        }
    }

    // If private/public is not present, default is always private
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
