using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [Header("Score Items")]
    [SerializeField] int score = 50;
    [SerializeField] int hitPoints = 5;

    [Header("Misc Items")]
    [SerializeField] Transform parentTransform;
    [SerializeField] GameObject explosionFX;
    [SerializeField] [Tooltip("seconds")] float explosionLifeTime = 2f;

    // cached parameters
    MeshRenderer myMeshRenderer;
    MeshCollider myMeshCollider;
    ScoreBoard scoreBoard;

    // state parameters
    bool isAlive = true;
    int currentHits = 0;

    // book keeping parameters
    GameObject explosion;

    private void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        scoreBoard = FindObjectOfType<ScoreBoard>();

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
            ++currentHits;
            if (currentHits < hitPoints)
            {
                return;
            }

            Kill();
        }
    }

    private void Kill()
    {
        isAlive = false;
        myMeshCollider.enabled = false;
        myMeshRenderer.enabled = false;

        foreach (Transform child in transform)
        {
            var emission = child.GetComponent<ParticleSystem>().emission;
            emission.enabled = false;
        }

        scoreBoard.AddToScore(score);

        StartCoroutine(ExplosionFX());
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
