using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;
    bool m_IsPatrol = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = CaculateDistacne(player.transform.position, transform.position);

        if (m_IsPatrol)
        {
            if (dist <= 4.5f)
            {
                m_IsPatrol = false;
                navMeshAgent.SetDestination(player.transform.position);
                return;
            }

            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }
        else
        {
            if (dist > 4.5f)
            {
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_IsPatrol = true;
            }
        }

        
    }

    float CaculateDistacne(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
}
