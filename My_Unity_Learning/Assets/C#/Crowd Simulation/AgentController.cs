// for "Crowd Simulation_1" Scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public GameObject firstGoal;
    public GameObject nextGoal;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(firstGoal.transform.position);
    }

    void Update()
    {
        if (Vector3.Distance(this.gameObject.transform.position, firstGoal.transform.position) < 1)
        {
            agent.SetDestination(nextGoal.transform.position);
        }
    }
}
