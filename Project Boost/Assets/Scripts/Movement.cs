using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, typically set in the editor
    //CACHE - references for readability and speed
    //STATE - private instance (member) variables

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        } else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateCounterClockwise();

        } else if (Input.GetKey(KeyCode.D))
        {
            RotateClockwise();
        } else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void RotateCounterClockwise()
    {
        ApplyRotation(rotateSpeed);

        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    private void RotateClockwise()
    {
        ApplyRotation(-rotateSpeed);

        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    private void StopRotating()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
