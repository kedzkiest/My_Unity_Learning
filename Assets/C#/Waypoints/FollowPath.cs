using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private Transform goal;

    public float speed = 5.0f;

    public float accuracy = 1.0f;

    public float rotSpeed = 2.0f;

    public GameObject wpManager;

    private GameObject[] wps;

    private GameObject currentNode;
    private int currentWP = 0;
    private Graph g;
    
    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
    }

    public void GoBehindTheBigRock()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 0;
    }

    public void GoCenter()
    {
        g.AStar(currentNode, wps[4]);
        currentWP = 0;
    }
    
    public void GoSounthernmost()
    {
        g.AStar(currentNode, wps[9]);
        currentWP = 0;
    }
    
    public void GoUnderTheTallTree()
    {
        g.AStar(currentNode, wps[6]);
        currentWP = 0;
    }
    
        // Update is called once per frame
    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
        {
            return;
        }
        
        //the node we are closest at this moment
        currentNode = g.getPathPoint(currentWP);
        
        //if we are close enough to the current waypoint move to next
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        {
            currentWP++;
        }
        
        //if we are not at the end of the path
        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction),
                Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
