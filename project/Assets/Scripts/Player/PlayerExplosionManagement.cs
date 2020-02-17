using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionManagement : MonoBehaviour
{
	enum State
	{
		Explosion,
		WaitMyExplosion,
		WaitChildrenExplosion
	}

	public Transform haveFireObject { get { return haveFireIndex < 0 ? transform : transform.GetChild(haveFireIndex); } }
	public int haveFireIndex { get; private set; } = -1;
	public float explosionElapasedTime { get { return m_timer.elapasedTime; } }
	public float explosionLimitTime { get { return m_explosionLimit; } }
	public bool isPlayerDeath { get; private set; } = false;

	[SerializeField]
	GameObject m_particleObject = null;
	[SerializeField]
	Rigidbody m_rigidbody = null;
	[SerializeField]
	List<MeshRenderer> m_renderers = new List<MeshRenderer>();
	[SerializeField]
	float m_explosionLimit = 10.0f;
	[SerializeField]
	float m_delayExplosionChildren = 0.1f;
	[SerializeField]
	AudioSource m_exolosionSe = null;

	ParticleSystem m_particle = null;
	Timer m_timer = new Timer();
	Timer m_waitTimer = new Timer();
	State m_state = State.Explosion;
	float m_myWaitTime = 0.0f;
	bool m_isExplosion = false;
	bool m_isGameClear = false;

	public void Explosion(bool isGameClear = false)
	{
		m_isGameClear = isGameClear;
		m_isExplosion = true;
		isPlayerDeath = true;
		GameSceneManager.instance.GameEnd();
	}

	public void AddChildren()
	{
		m_timer.Start();
		++haveFireIndex;
	}
	
	//死ぬ, 子供とともに爆発, 火の管理
	// Start is called before the first frame update
	void Start()
    {
		GameSceneManager.instance.SetPlayerObject(gameObject);
		GameSceneManager.instance.SetPlayerExplosionManagement(this);
		TakeCollision.callback += AddChildren;
		GameSceneManager.instance.gameStartCallback += m_timer.Start;
	}

	void FixedUpdate()
	{
		if (m_isExplosion)
			m_rigidbody.velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update()
    {
		if (!GameSceneManager.instance.isInGame & !isPlayerDeath)
			return;

		if (transform.position.y < -50.0f)
			Explosion();

		if (Input.GetKeyDown(KeyCode.P))
			Explosion();

        if (!m_isExplosion && m_timer.elapasedTime >= m_explosionLimit)
			Explosion();
		else if (m_isExplosion)
		{
			switch(m_state)
			{
				case State.Explosion:
				{
					float waitTime = 0.0f;
					m_state = State.WaitChildrenExplosion;

					for (int i = 0; i < transform.childCount; ++i)
					{
						var get = transform.GetChild(i).GetComponent<Bomb>();
						if (get != null)
						{
							get.Boom(waitTime);
							waitTime += m_delayExplosionChildren;
						}
					}

					m_myWaitTime = waitTime;
					m_waitTimer.Start();
					break;
				}
				case State.WaitChildrenExplosion:
				{
					if (m_waitTimer.elapasedTime >= m_myWaitTime)
					{		
						var obj = Instantiate(m_particleObject);

						foreach (var e in m_renderers)
							e.enabled = false;
						obj.transform.rotation = Quaternion.identity;
						obj.transform.position = transform.position;
						m_particle = obj.GetComponent<ParticleSystem>();
						m_rigidbody.velocity = Vector3.zero;

						if (m_exolosionSe != null) m_exolosionSe.PlayOneShot(m_exolosionSe.clip);
						m_state = State.WaitMyExplosion;
					}
					break;
				}

				case State.WaitMyExplosion:
					{
						if (!m_particle.IsAlive())
						{
							Destroy(gameObject);
							if (!m_isGameClear) GameSceneManager.instance.GameOver();
							else GameSceneManager.instance.GameClear();
						}
						break;
					}
				default:
					break;
			}
		}
    }
}
