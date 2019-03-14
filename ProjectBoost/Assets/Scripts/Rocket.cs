﻿using System.Collections;
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
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            ProcessInput();
        }
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
            ToggleThrustSFX(true);
            myRigidBody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        }

        ToggleThrustSFX(isThrusting);
    }

    private void Rotate()
    {
        myRigidBody.freezeRotation = true; // take manual control of the rotation

        float rotationThrust = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rotationThrust);
        }

        myRigidBody.freezeRotation = false; // resume physics control of the rotation
    }

    private void ToggleThrustSFX(bool flag)
    {
        myAudioSource.enabled = flag;
    }

    private void PlayDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathClipVolume);
    }

    private void PlaySuccessSFX()
    {
        AudioSource.PlayClipAtPoint(successClip, Camera.main.transform.position, successClipVolume);
    }

    void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
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
                Debug.Log("Friendly");
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
        if (collision.gameObject.tag != "Friendly")
        {
            Debug.Log("Ouch");
        }
    }

    private void FinishSequence()
    {
        isAlive = false;
        PlaySuccessSFX();
        levelLoader.LoadNextLevel();
    }

    private void DeathSequence()
    {
        isAlive = false;
        PlayDeathSFX();
        levelLoader.ReloadLevel();
    }
}
