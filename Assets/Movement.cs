using System.Collections;
using System.Collections.Generic;
using UnityEngine; // namespace where MonoBehavior data lives

public class Movement : MonoBehaviour
{
    // PARAMETERS -- For tuning, typically set in the editor;
    // CACHE -- e.g. refrences for readability or speed;
    // STATE -- private instance (member) variables;

    // Member variable (available throughout entire class)
    [SerializeField] float thrustForce;
    [SerializeField] float rotationThrust;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start() {
        // Caches a reference to our component
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        // Use KeyCode.name signature since (string name) sig can be tricky to get correct
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBoosterParticles.isPlaying) {
            mainBoosterParticles.Play();
        }
    }
    
    private void StopThrusting() {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A)) {
            RotateLeft();
        } else if (Input.GetKey(KeyCode.D)) {
            RotateRight();
        } else {
            StopRotating();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
            if (!rightBoosterParticles.isPlaying)
            {
                rightBoosterParticles.Play();
            }
    }

    private void RotateRight() {
        ApplyRotation(-rotationThrust);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    private void StopRotating() {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
