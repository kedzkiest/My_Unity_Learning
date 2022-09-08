using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform enemy;
    private Vector3 vec;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            vec = new Vector3(-1, 0, 0);
            enemy.transform.Translate(vec * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            vec = new Vector3(1, 0, 0);
            enemy.transform.Translate(vec * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            vec = new Vector3(0, 0, 1);
            enemy.transform.Translate(vec * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        { 
            vec = new Vector3(0, 0, -1);
            enemy.transform.Translate(vec * speed * Time.deltaTime);
        }
    }
}
