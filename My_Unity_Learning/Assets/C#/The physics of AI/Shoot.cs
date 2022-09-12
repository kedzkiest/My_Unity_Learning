using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    public GameObject bulletSpawnPosition;

    public GameObject target;
    public GameObject parent;

    public float turnSpeed;
    
    public float speed;

    private bool canShoot = true;

    public float canShootInterval;

    public bool trajectoryMode = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CanShootAgain()
    {
        canShoot = true;
    }
    
    void fire()
    {
        if (canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition.transform.position,
                bulletSpawnPosition.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = speed * this.transform.forward;
            canShoot = false;
            Invoke("CanShootAgain", canShootInterval);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * 30, Color.red);

        Vector3 direction = (target.transform.position - parent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        parent.transform.rotation =
            Quaternion.Slerp(parent.transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        float? angle = RotateTurret();

        if (angle != null && Vector3.Angle(direction, parent.transform.forward) < 10)
        {
            fire();
        }

    }
    
    float? RotateTurret()
    {
        if (Input.GetKey(KeyCode.L))
        {
            trajectoryMode = true;
        }

        if (Input.GetKey(KeyCode.H))
        {
            trajectoryMode = false;
        }
        
        float? angle = CalculateAngle(trajectoryMode);

        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float) angle, 0, 0);
        }

        return angle;
    }
    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float gravity = 9.81f;
        float sSqr = speed * speed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float highAngle = sSqr + root;
            float lowAngle = sSqr - root;

            if (low)
            {
                return (Mathf.Atan2(lowAngle, gravity * x) * Mathf.Rad2Deg);
            }
            else
            {
                return (Mathf.Atan2(highAngle, gravity * x) * Mathf.Rad2Deg);
            }
        }
        else
        {
            return null;
        }
    }
}
