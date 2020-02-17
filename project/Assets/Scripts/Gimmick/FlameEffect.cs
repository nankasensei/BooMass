using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    public float onTime;
    public float offTime;
    public GameObject effect;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time <= onTime)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            effect.SetActive(true);

        }
        else if (time <= onTime + offTime)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            effect.SetActive(false);

        }
        else
        {
            time = 0.0f;
        }
    }
}
