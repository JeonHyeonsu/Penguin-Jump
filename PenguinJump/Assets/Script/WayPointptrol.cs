using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointptrol : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] waypoints;
    int waypointIndex = 0;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }


    void Update()
    {
        MovePath();

    }

    public void MovePath()
    {

        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex++;
        }

        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }

    }
}
