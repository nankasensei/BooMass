using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCollision : MonoBehaviour
{
	public static System.Action callback = null;

	[SerializeField]
	LayerMaskEx m_hitLayer = 0;

	bool m_isDestroy = false;
	
	void OnCollisionEnter(Collision collision)
	{
		if (!m_isDestroy && m_hitLayer.EqualBitsForGameObject(collision.gameObject) && (transform.position - collision.gameObject.transform.position).magnitude < 0.8f)
		{
			Destroy(transform.GetComponent<Rigidbody>());
			m_isDestroy = true;
			callback?.Invoke();

			transform.parent = collision.gameObject.transform;
			
			collision.gameObject.GetComponent<BombBlink>().targetRenderer = GetComponent<Renderer>();
		}
    }	
}
