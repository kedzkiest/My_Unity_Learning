using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    public Transform player;
    private int currentWP = 0;

    public float speed = 10.0f;
    public float rotateSpeed = 10.0f;
    public float lookAhead = 10.0f;

    private GameObject tracker;
    
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = player.transform.position;
        tracker.transform.rotation = player.transform.rotation;
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(tracker.transform.position, player.transform.position) > lookAhead) return;
        
        if(Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
        {
            currentWP++;
        }

        if (currentWP >= waypoints.Length)
        {
            currentWP = 0;
        }
        
        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 20) * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(player.transform.position, player.forward * 30, Color.green, 0.3f);

        ProgressTracker();

        
        Quaternion lookAtWP =
            Quaternion.LookRotation(tracker.transform.position - player.transform.position);
        player.transform.rotation = Quaternion.Slerp(transform.rotation, lookAtWP, rotateSpeed * Time.deltaTime);
        player.transform.Translate(0, 0, speed * Time.deltaTime);
        
    }
}
