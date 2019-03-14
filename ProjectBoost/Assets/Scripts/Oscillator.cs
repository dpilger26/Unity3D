using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // only allows this script to be added once per object
public class Oscillator : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Vector3 movementVector = new Vector3(0, 1, 0);
    [SerializeField] float movementSpeed = 5f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + movementVector * Mathf.Sin(Time.fixedTime * movementSpeed);
    }
}
