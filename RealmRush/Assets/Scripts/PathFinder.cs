using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Waypoint startWaypoint;
    [SerializeField] Waypoint endWaypoint;

    // class members
    Dictionary<Vector3Int, Waypoint> grid = new Dictionary<Vector3Int, Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(var waypoint in waypoints)
        {
            var waypointGridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(waypointGridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint.name);
            }
            else
            {
                grid.Add(waypointGridPos, waypoint);
            }
        }
    }

    public bool IsStart(Waypoint waypoint)
    {
        return waypoint.Equals(startWaypoint);
    }

    public bool IsEnd(Waypoint waypoint)
    {
        return waypoint.Equals(endWaypoint);
    }
}
