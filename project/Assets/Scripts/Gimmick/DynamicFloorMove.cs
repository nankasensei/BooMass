using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicFloorMove : MonoBehaviour
{
    public float speed;
    public float distance;
    public float stopTime;

    private float time;
    private float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        moveTime = distance / speed;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= moveTime)
        {
            transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        }
        else if (time <= moveTime + stopTime)
        {
            //nop
        }
        else if (time <= 2 * moveTime + stopTime)
        {
            transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
        }
        else if (time <= 2 * (moveTime + stopTime))
        {
            //nop
        }
        else
        {
            time = 0.0f;
        }
    }
}
