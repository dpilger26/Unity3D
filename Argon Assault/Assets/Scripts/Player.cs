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

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float newXPos = Mathf.Clamp(transform.localPosition.x + xOffset, minXOffset, maxXOffset);
        float newYPos = Mathf.Clamp(transform.localPosition.y + yOffset, minYOffset, maxYOffset);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}
