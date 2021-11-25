using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform goal;
    public float accuracy = 0.1f;
    public float speed = 0.1f;

    public DisplayDist displayDist;
    private float dist;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(goal.position);
        Vector3 direction = goal.position - this.transform.position;
        Debug.DrawRay(this.transform.position, direction, Color.red);

        dist = displayDist.GetDist();
        
        if (direction.magnitude > accuracy && dist < 5)
        {
            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }
    
    
}