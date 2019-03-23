using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // configuration parameters
    [Header("Movement Parameters")]
    [SerializeField] [Tooltip("Local coordinates")] float minXOffset = -5f;
    [SerializeField] [Tooltip("Local coordinates")] float maxXOffset = 5f;
    [SerializeField] [Tooltip("Local coordinates")] float minYOffset = -5f;
    [SerializeField] [Tooltip("Local coordinates")] float maxYOffset = 5f;
    [SerializeField] [Tooltip("In m/s")] float xSpeed = 20f;
    [SerializeField] [Tooltip("In m/s")] float ySpeed = 20f;
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 4f;
    [SerializeField] float controlRollFactor = -20f;

    // state parameters
    float xThrow;
    float yThrow;

    // Update is called once per frame
    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, minXOffset, maxXOffset);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, minYOffset, maxYOffset);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    private void Rotate()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControl;

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
