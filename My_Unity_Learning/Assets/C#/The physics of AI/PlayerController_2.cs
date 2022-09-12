using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2 : MonoBehaviour
{
    public Transform player;
    public float speed;

    public float rotateSpeed;

    private Vector3 vec;
    public int moveMode = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            moveMode = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            moveMode = 2;
        }
        
        Debug.DrawRay(this.transform.position, this.transform.forward * 30, Color.green);
        
        if (moveMode == 1)
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
        }
        else if (moveMode == 2)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                vec = new Vector3(0, -1, 0);
                player.transform.Rotate(vec * rotateSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                vec = new Vector3(0, 1, 0);
                player.transform.Rotate(vec * rotateSpeed * Time.deltaTime);
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

        }

    }
}
