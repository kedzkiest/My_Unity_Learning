using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject explosion;
    private Rigidbody rb;

    /*public float mass;
    public float force;
    private float acceleration;
    
    public float gravity = -9.8f;
    private float gAcceleration;
    private float speedZ;
    private float speedY;
    */
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject exp = Instantiate(explosion, this.transform);
            Destroy(exp,0.3f);
            Destroy(this.gameObject, 0.3f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 8.0f);
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        /*acceleration = force / mass;
        speedZ += acceleration * Time.deltaTime;

        gAcceleration = gravity / mass;
        speedY += gAcceleration * Time.deltaTime;
        this.transform.Translate(0, speedY, speedZ);

        force = 0;
        */

        this.transform.forward = rb.velocity;

    }
}
