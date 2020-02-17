using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanJump : MonoBehaviour
{
	[SerializeField]
	BoxCastFlags m_flags = null;
	[SerializeField]
	Rigidbody m_rigidBody = null;
	[SerializeField]
	float m_moveTime = 2;
	[SerializeField]
	float m_power = 1.0f;

	Timer m_timer = new Timer();
	bool m_isJump = false;
	bool m_isUp = true;

	// Update is called once per frame
	void Update()
	{
		if (GameSceneManager.instance.isGameClear && !m_isJump)
		{
			m_isJump = true;
			m_rigidBody.useGravity = false;
			m_timer.Start();
		}

		if (m_isJump)
		{
			if (m_isUp)
			{
				transform.position += Vector3.up * m_power * Time.deltaTime;

				if (m_timer.elapasedTime > m_moveTime)
				{
					m_isUp = false;
					m_rigidBody.useGravity = true;
				}
			}
			else
			{
				if (m_flags.isStay)
				{
					m_isUp = true;
					m_rigidBody.useGravity = false;
					m_timer.Start();
				}
			}
		}
	}
}
