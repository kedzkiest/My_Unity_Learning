//for "Crowd Simulation_3" Scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private GameObject[] goalLocations;
    private NavMeshAgent agent;

    private Animator anim;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int WOffset = Animator.StringToHash("wOffset");
    private static readonly int SpeedMult = Animator.StringToHash("speedMult");

    private float speedMult;
    private float detectionRadius = 15;
    private float fleeRadius = 10;

    void ResetAgent()
    {
        speedMult = Random.Range(0.5f, 1.5f);
        agent.speed = 2 * speedMult;
        agent.angularSpeed = 120;
        anim.SetFloat(SpeedMult, speedMult);
        anim.SetTrigger(IsWalking);
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newGoal = this.transform.position + fleeDirection * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger(IsRunning);
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        anim = GetComponent<Animator>();
        anim.SetFloat(WOffset, Random.Range(0, 1));
        ResetAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }
}