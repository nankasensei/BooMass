using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingForTitle : MonoBehaviour
{
    public float period;
    public AnimationCurve blinkCurve;

    private Text text;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float x = 0;
        if(time > period)
        {
            x = 1;
            time = 0;
        }
        else
        {
            x = time / period;
        }

        var tmpColor = text.color;
        tmpColor.a = blinkCurve.Evaluate(x);
        text.color = tmpColor;
    }
}
