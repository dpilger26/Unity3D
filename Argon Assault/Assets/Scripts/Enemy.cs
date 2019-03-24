using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [SerializeField] GameObject explosionFX;
    [SerializeField] [Tooltip("seconds")] float explosionLifeTime = 2f;

    // book keeping parameters
    GameObject explosion;

    private void OnParticleCollision(GameObject other)
    {
        explosion = Instantiate(explosionFX, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
    }

    IEnumerator ExplosionFX()
    {
        yield return new WaitForSecondsRealtime(explosionLifeTime);
        Destroy(explosion);
    }
}
