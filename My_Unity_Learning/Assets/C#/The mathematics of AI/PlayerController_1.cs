using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_1 : MonoBehaviour
{
    public Transform player;
    public float speed = 0.1f;
    public float rotateSpeed = 0.1f;
    public float autoSpeed = 0.1f;
    private Vector3 vec;

    public DisplayDist displayDist;
    public int moveMode = 1;
    private int previousMoveMode;

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
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            moveMode = 3;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Quaternion angle = Quaternion.identity;
            
            player.transform.position = new Vector3(Random.Range(-40, 41), player.position.y, Random.Range(-40, 41));
            angle.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            player.transform.rotation = angle;
        }
        
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
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.transform.Rotate(0, displayDist.angle * displayDist.clockwise,  0);
            }

        }
        else if (moveMode == 3)
        {
            if (displayDist.dist > 0.5f)
            {
                player.transform.Rotate(0, displayDist.angle * displayDist.clockwise * 0.05f,  0);
                player.transform.Translate(player.transform.forward * autoSpeed * Time.deltaTime, Space.World);
                
            }
        }
    }
}