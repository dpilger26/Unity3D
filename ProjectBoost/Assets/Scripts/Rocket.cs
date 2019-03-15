using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // configuration parameters
    [Header("Thrust Parameters")]
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rcsThrust = 100f;

    [Header("Audio Parameters")]
    [SerializeField] AudioClip deathClip;
    [SerializeField] [Range(0, 1)] float deathClipVolume = 1f;
    [SerializeField] AudioClip successClip;
    [SerializeField] [Range(0, 1)] float successClipVolume = 1f;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;

    // cached references
    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    LevelLoader levelLoader;

    // state parameters
    bool isAlive = true;

    // Start is called before the first frame update
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();

        levelLoader = FindObjectOfType<LevelLoader>();
        ToggleThrustVFX(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            ProcessInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    private void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        bool isThrusting = Input.GetKey(KeyCode.Space);

        if (isThrusting)
        {
            myRigidBody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }

        ToggleFX(isThrusting);
    }

    private void Rotate()
    {
        float rotationThrust = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThrust)
    {
        myRigidBody.freezeRotation = true; // take manual control of the rotation
        transform.Rotate(Vector3.forward * rotationThrust);
        myRigidBody.freezeRotation = false; // resume physics control of the rotation
    }

    private void ToggleThrustSFX(bool isThrusting)
    {
        myAudioSource.enabled = isThrusting;
    }

    private void ToggleThrustVFX(bool isThrusting)
    {
        if (isThrusting)
        {
            thrustParticles.Play();
        }
        else
        {
            thrustParticles.Stop();
        }
    }

    private void ToggleFX(bool isThrusting)
    {
        ToggleThrustSFX(isThrusting);
        ToggleThrustVFX(isThrusting);
    }

    private void PlayDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathClipVolume);
    }

    private void PlaySuccessSFX()
    {
        AudioSource.PlayClipAtPoint(successClip, Camera.main.transform.position, successClipVolume);
    }

    private void HandleCollision(Collision collision)
    {
        if (!isAlive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
            {
                // do nothing right now
                break;
            }
            case "Finish":
            {
                FinishSequence();
                break;
            }
            default:
            {
                DeathSequence();
                break;
            }

        }
    }

    private void FinishSequence()
    {
        isAlive = false;
        deathParticles.Play();
        PlaySuccessSFX();
        levelLoader.LoadNextLevel();
    }

    private void DeathSequence()
    {
        isAlive = false;
        successParticles.Play();
        PlayDeathSFX();
        levelLoader.ReloadLevel();
    }
}
