using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.name);
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        Debug.Log("Player Dying");
        SendMessage("OnPlayerDeath");
    }
}
