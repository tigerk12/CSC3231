using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointSpawner : MonoBehaviour
{

    public List<Transform> Waypoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        GetWayPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 RandomWayPoint()
    {
        int randomWP = Random.Range(0, (Waypoints.Count - 1));
        Vector3 randomWayPoint = Waypoints[randomWP].transform.position;
        return randomWayPoint;
    }


    void GetWayPoints()
    {
        Transform[] wpList = this.transform.GetComponentsInChildren<Transform>();

        for(int i = 0; i< wpList.Length; i++)
        {
            if (wpList[i].tag == "waypoint")
            {
                Waypoints.Add(wpList[i]);
            }
        }
    }
}
