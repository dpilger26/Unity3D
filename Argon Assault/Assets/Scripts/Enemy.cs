using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Transform parentTransform;
    [SerializeField] GameObject explosionFX;
    [SerializeField] [Tooltip("seconds")] float explosionLifeTime = 2f;

    // cached parameters
    MeshRenderer myMeshRenderer;
    MeshCollider myMeshCollider;

    // state parameters
    bool isAlive = true;

    // book keeping parameters
    GameObject explosion;

    private void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        AddNonTriggerMeshCollider();
    }

    private void AddNonTriggerMeshCollider()
    {
        myMeshCollider = gameObject.AddComponent<MeshCollider>();
        myMeshCollider.convex = true;
        myMeshCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (isAlive)
        {
            isAlive = false;
            myMeshCollider.enabled = false;
            myMeshRenderer.enabled = false;
            StartCoroutine(ExplosionFX());
        }
    }

    IEnumerator ExplosionFX()
    {
        explosion = Instantiate(explosionFX, transform.position,
            Quaternion.identity, parentTransform) as GameObject;
        yield return new WaitForSecondsRealtime(explosionLifeTime);
        Destroy(explosion);
        Destroy(gameObject);
    }
}
