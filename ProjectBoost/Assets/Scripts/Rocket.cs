using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // configuration parameters
    [Header("Thrust Parameters")]
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rcsThrust = 100f;

    // cached references
    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    LevelLoader levelLoader;

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
        ProcessInput();
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

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
            {
                Debug.Log("Friendly");
                break;
            }
            case "Finish":
            {
                Debug.Log("Finish");
                levelLoader.LoadNextLevel();
                break;
            }
            default:
            {
                Debug.Log("Ouch");
                levelLoader.ReloadLevel();
                break;
            }

        }
        if (collision.gameObject.tag != "Friendly")
        {
            Debug.Log("Ouch");
        }
    }
}
