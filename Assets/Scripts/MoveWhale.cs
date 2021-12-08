using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhale : MonoBehaviour
{
    public GameObject water;
    private WayPointSpawner m_pointSpawner;

    private bool m_hasTarget = false;
    private bool m_isTurning;

    private Vector3 m_wayPoint;
    private Vector3 m_lastWayPoint = new Vector3(0f, 0f, 0f);

    private float m_speed;

    // Start is called before the first frame update
    void Start()
    {
        m_pointSpawner = water.transform.GetComponent<WayPointSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_hasTarget)
        {
            m_hasTarget = canFindTarget();
        }
        else
        {
            RotateNPC(m_wayPoint, m_speed);
            transform.position = Vector3.MoveTowards(transform.position, m_wayPoint, m_speed * Time.deltaTime);
        }

        if (transform.position == m_wayPoint)
            m_hasTarget = false;
    }

    bool canFindTarget(float start = 5f, float end = 20f)
    {
        m_wayPoint = m_pointSpawner.RandomWayPoint();

        if(m_lastWayPoint == m_wayPoint)
        {
            m_wayPoint = m_pointSpawner.RandomWayPoint();
            return false;
        }
        else
        {
            m_lastWayPoint = m_wayPoint;
            m_speed = Random.Range(start, end);
            return true;
        }
    }

    void RotateNPC (Vector3 waypoint, float currentSpeed)
    {
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f);

        Vector3 LookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-LookAt), TurnSpeed * Time.deltaTime);
    }
}
