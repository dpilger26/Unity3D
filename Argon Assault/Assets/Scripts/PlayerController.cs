﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    // configuration parameters
    [Header("Limit Parameters")]
    [SerializeField] [Tooltip("Local coordinates")] float minXOffset = -8f;
    [SerializeField] [Tooltip("Local coordinates")] float maxXOffset = 8f;
    [SerializeField] [Tooltip("Local coordinates")] float minYOffset = -5f;
    [SerializeField] [Tooltip("Local coordinates")] float maxYOffset = 5f;

    [Header("Position Parameters")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5.5f;

    [Header("Control Parameters")]
    [SerializeField] [Tooltip("In m/s")] float xSpeed = 12f;
    [SerializeField] [Tooltip("In m/s")] float ySpeed = 12f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;

    // state parameters
    float xThrow = 0f;
    float yThrow = 0f;
    bool isControlEnabled = true;

    // Update is called once per frame
    private void Update()
    {
        if (isControlEnabled)
        {
            Move();
            Rotate();
        }
    }

    private void OnPlayerDeath() // Called by string reference elsewhere
    {
        Debug.Log("Disabling player controls.");
        isControlEnabled = false;
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