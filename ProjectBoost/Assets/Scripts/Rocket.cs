using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // configuration parameters
    [Header("Thrust Parameters")]
    [SerializeField] float thrust = 5f;
    [SerializeField] float rcsThrust = 5f;

    // cached references
    Rigidbody myRigidBody;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInput();
        ToggleThrustSFX(Input.GetKey(KeyCode.Space));
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ToggleThrustSFX(true);
            myRigidBody.AddRelativeForce(Vector3.up * thrust);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rcsThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * rcsThrust);
        }
    }

    private void ToggleThrustSFX(bool flag)
    {
        myAudioSource.enabled = flag;
    }
}
