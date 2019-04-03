using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // configuration parameters
    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        Debug.Log("Starting Patrol");
        foreach (var waypoint in path)
        {
            Debug.Log("Visiting " + waypoint.name);
            transform.position = waypoint.transform.position;
            yield return new WaitForSecondsRealtime(1f);
        }
        Debug.Log("Ending Patrol");
    }
}
