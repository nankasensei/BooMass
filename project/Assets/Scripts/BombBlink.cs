using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BombBlink : MonoBehaviour
{
    public Renderer targetRenderer
    {
        get
        {
            return _targetRenderer;
        }
        set
        {
            if (_targetRenderer != null)
            {
                _targetRenderer.material.SetColor(emissionColorId, originalColor);
                foreach (var e in childrens)
                    e.material.SetColor(emissionColorId, originalColor);

            }
            _targetRenderer = value;
            childrens = _targetRenderer.GetComponentsInChildren<Renderer>().Where(c => gameObject != c.gameObject).ToArray();

            if (_targetRenderer != null)
            {
                //originalColor = _targetMaterlal.color;
                originalColor = _targetRenderer.material.GetColor(emissionColorId);
            }
            resetValues();
        }
    }
    public Color redColor;
    public AnimationCurve blinkCurve;
    public float blinkPeriodMax;
    public float blinkPeriodMin;

	private Color originalColor;
    private float blendRateTime;
    private bool isAdd;
    private Renderer _targetRenderer;
	private PlayerExplosionManagement playerExplosionManagement;

	private Renderer[] childrens = null;


	private readonly int emissionColorId = Shader.PropertyToID("_EmissionColor");

	public void SetChildrens(Renderer[] set)
	{
		foreach(var e in childrens)
			e.material.SetColor(emissionColorId, originalColor);
		childrens = set;
	}


    private void resetValues()
    {
        blendRateTime = 0;
        isAdd = false;
    }

    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        resetValues();
        playerExplosionManagement = GetComponent<PlayerExplosionManagement>();
		childrens = GetComponentsInChildren<Renderer>().Where(c => gameObject != c.gameObject).ToArray();
	}



    // Update is called once per frame
    void Update()
    {
        if(targetRenderer == null || !GameSceneManager.instance.isInGame) { return; }

        var timeLeft = playerExplosionManagement.explosionLimitTime - playerExplosionManagement.explosionElapasedTime;

        var blinkPeriod = blinkPeriodMin + (blinkPeriodMax - blinkPeriodMin) / playerExplosionManagement.explosionLimitTime * timeLeft;

        var addTime = Time.deltaTime * (isAdd ? 1f : -1f);
        blendRateTime += addTime;

        if(blendRateTime < 0)
        {
            isAdd = true;
            blendRateTime = 0;
        }else if(blendRateTime > blinkPeriod)
        {
            isAdd = false;
            blendRateTime = blinkPeriod;
        }

        var blendRate = blinkCurve.Evaluate(blendRateTime / blinkPeriod);
        //targetMaterlal.color = Color.Lerp(originalColor, redColor, blendRate);
        targetRenderer.material.SetColor(emissionColorId, Color.Lerp(originalColor, redColor, blendRate));
		foreach(var e in childrens)
			e.material.SetColor(emissionColorId, Color.Lerp(originalColor, redColor, blendRate));
	}
}
