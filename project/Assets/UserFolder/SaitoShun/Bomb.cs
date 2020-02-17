using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomb : MonoBehaviour
{
	public bool isExplosion { get { return BoomFlag > 0; } }

	[SerializeField]
	GameObject m_particleObject = null;
	[SerializeField]
	AudioSource m_explosionSE = null;
	[SerializeField]
	List<MeshRenderer> m_renderers = new List<MeshRenderer>();


    private ParticleSystem BoomEffect;
    private Timer timer;

    private float delayTime;
    private int BoomFlag = 0;

    private void Start()
    {
        timer = new Timer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.elapasedTime > delayTime && BoomFlag == 1) {
            BoomFlag = 2;

			if (m_particleObject != null)
			{
				var obj = Instantiate(m_particleObject);
				obj.transform.position = transform.position;
				obj.transform.rotation = Quaternion.identity;
				BoomEffect = obj.GetComponent<ParticleSystem>();
			}
			if (m_explosionSE != null)
				m_explosionSE.PlayOneShot(m_explosionSE.clip);

			foreach (var e in m_renderers)
				e.enabled = false;
		}

		if (BoomFlag == 2 && !BoomEffect.IsAlive() ) {
            Destroy(this.gameObject);
        }
    }

    public void Boom(float delayTime) {


		// timer = new Timer();
			
        timer.Start();

        this.delayTime = delayTime;
        BoomFlag = 1;
		Debug.Log("Boom function start : " + delayTime);

	}
}
