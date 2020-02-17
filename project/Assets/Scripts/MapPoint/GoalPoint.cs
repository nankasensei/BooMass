using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
	public static System.Action gameNotClearCallback { get; set; } = null;

	[SerializeField]
	GameObject m_activeObject = null;
	[SerializeField]
	LayerMaskEx m_hitLayer = 1;
	[SerializeField]
	bool m_isTimeStop = false;

	bool m_isSettings = false;

	void OnTriggerEnter(Collider other)
	{
		if (!m_isSettings && m_hitLayer.EqualBitsForGameObject(other.gameObject))
		{
			var component = other.transform.GetComponent<PlayerExplosionManagement>();
			if (component == null) return;

			if (GameSceneManager.instance.isPlayerAllHaveBoms)
			{
				if (m_isTimeStop) Time.timeScale = 0.0f;
				if (m_activeObject != null) m_activeObject.SetActive(true);

				component.Explosion(true);
				m_isSettings = true;
			}
			else
			{
				gameNotClearCallback?.Invoke();
			}
		}
	}
}
