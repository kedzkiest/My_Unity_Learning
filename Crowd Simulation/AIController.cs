//for "Crowd Simulation_2" Scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private GameObject[] goalLocations;
    private NavMeshAgent agent;

    private Animator anim;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int WOffset = Animator.StringToHash("wOffset");
    private static readonly int SpeedMult = Animator.StringToHash("speedMult");

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = GetComponent<Animator>();
        anim.SetFloat(WOffset, Random.Range(0, 1));
        anim.SetTrigger(IsWalking);
        float sm = Random.Range(0.5f, 1.5f);
        anim.SetFloat(SpeedMult, sm);
        agent.speed *= sm;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }
}
