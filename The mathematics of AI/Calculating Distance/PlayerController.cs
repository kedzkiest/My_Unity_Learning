using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public float speed = 0.1f;
    private Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vec = new Vector3(-1, 0, 0);
            player.transform.Translate(vec * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vec = new Vector3(1, 0, 0);
            player.transform.Translate(vec * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            vec = new Vector3(0, 0, 1);
            player.transform.Translate(vec * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            vec = new Vector3(0, 0, -1);
            player.transform.Translate(vec * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            vec = new Vector3(0, 1, 0);
            player.transform.Translate(vec * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vec = new Vector3(0, -1, 0);
            player.transform.Translate(vec * speed * Time.deltaTime);
        }
    }
}