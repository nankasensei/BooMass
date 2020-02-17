using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWallMove : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed;
    public float stopTime;

    private float time;
    private float moveTime;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        distance = moveDirection.magnitude;
        moveTime = distance / speed;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= moveTime)
        {
            transform.position += Time.deltaTime * moveDirection;
        }
        else if (time <= moveTime + stopTime)
        {
            //nop
        }
        else if (time <= 2 * moveTime + stopTime)
        {
            transform.position -= Time.deltaTime * moveDirection;
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
