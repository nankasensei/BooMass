using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
	[SerializeField]
	Rigidbody m_rigidbody = null;
	[SerializeField]
	float m_force = 1000;
	[SerializeField]
	float y = 0.5f;

	private void Start()
	{
		GameSceneManager.instance.gameEndCallback += Explosion;
	}

	void Explosion()
	{
		Vector3 vec = (transform.position - GameSceneManager.instance.playerObject.transform.position).normalized;
		vec.y = y;
		m_rigidbody.AddForce(vec
			* m_force * Time.deltaTime, ForceMode.Impulse);
	}
}
