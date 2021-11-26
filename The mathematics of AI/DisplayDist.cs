using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayDist : MonoBehaviour
{
    public Transform player;
    public Transform box;
    public Text distText;
    public Text dotProductText;
    public Text crossProductText;

    public float dist;
    private float dotProduct;
    public float angle;
    public Vector3 crossProduct;
    public int clockwise = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 boxPos = box.transform.position;
        Debug.DrawRay(playerPos, player.forward * 30, Color.green, 0.3f);
        Debug.DrawRay(playerPos, boxPos - playerPos, Color.red);

        dist = calcDist(playerPos, boxPos);
        
        dotProduct = calcDotProduct(player.forward, boxPos - playerPos);
        angle = Mathf.Acos(dotProduct / (player.forward.magnitude * (boxPos-playerPos).magnitude)) * Mathf.Rad2Deg;
        
        crossProduct = calcCrossProduct(player.forward, boxPos - playerPos);
        
        if (crossProduct.y < 0)
        {
            clockwise = -1;
        }
        else
        {
            clockwise = 1;
        }
        distText.text = dist.ToString();
        dotProductText.text = angle.ToString();
        crossProductText.text = crossProduct.ToString();
    }
    float calcDist(Vector3 a, Vector3 b)
    {
        float ans;

        float x1 = a.x;
        float x2 = b.x;
        float y1 = a.y;
        float y2 = b.y;
        float z1 = a.z;
        float z2 = b.z;
        ans = Mathf.Sqrt((x2-x1)*(x2-x1) + (y2-y1)*(y2-y1) + (z2-z1)*(z2-z1));
        return ans;
    }

    float calcDotProduct(Vector3 a, Vector3 b)
    {
        float ans;

        float x1 = a.x;
        float x2 = b.x;
        float y1 = a.y;
        float y2 = b.y;
        float z1 = a.z;
        float z2 = b.z;
        ans = x1 * x2 + y1 * y2 + z1 * z2;
        return ans;
    }

    Vector3 calcCrossProduct(Vector3 a, Vector3 b)
    {
        Vector3 ans;

        float x1 = a.x;
        float x2 = b.x;
        float y1 = a.y;
        float y2 = b.y;
        float z1 = a.z;
        float z2 = b.z;

        ans = new Vector3(y1 * z2 - y2 * z1, z1 * x2 - z2 * x1, x1 * y2 - x2 * y1);
        return ans;
    }
    public float GetDist()
    {
        return dist;
    }
}