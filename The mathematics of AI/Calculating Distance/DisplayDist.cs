using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDist : MonoBehaviour
{
    public Transform player;
    public Transform box;
    public Text text;

    private float dist;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x1 = player.transform.position.x;
        float x2 = box.transform.position.x;
        float y1 = player.transform.position.y;
        float y2 = box.transform.position.y;
        float z1 = player.transform.position.z;
        float z2 = box.transform.position.z;
        
        dist = Mathf.Sqrt((x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) + (z2-z1)*(z2-z1));
        text.text = dist.ToString();
    }

    public float GetDist()
    {
        return dist;
    }
}