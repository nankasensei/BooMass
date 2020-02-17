using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneMovement : MonoBehaviour
{
	public enum MoveMode
	{
		EnterMovePrototypeScene,
		NumberMoveGameScenes
	}

	[SerializeField]
	MoveMode m_moveMode = MoveMode.EnterMovePrototypeScene;
	[SerializeField]
	AudioSource m_source = null;
	[SerializeField]
	float m_deley = 1.0f;

	int m_sceneIndex = -1;
	Timer m_timer = new Timer();

    // Update is called once per frame
    void Update()
    {
		if (!m_timer.isStart)
		{
			switch (m_moveMode)
			{
				case MoveMode.EnterMovePrototypeScene:
					{
						if (Input.GetKeyDown(KeyCode.Return))
						{
							m_sceneIndex = 0;
							m_timer.Start();
							m_source.PlayOneShot(m_source.clip);
						}
						break;
					}
				case MoveMode.NumberMoveGameScenes:
					{
						for (int i = 0; i < GameSceneManager.numMainScenes && i < 8; ++i)
						{
							if (Input.GetKeyDown(KeyCode.Alpha1 + i))
							{
								m_sceneIndex = i;
								m_timer.Start();
								m_source.PlayOneShot(m_source.clip);
							}
						}

						break;
					}
				default:
					break;
			}
		}
		else
		{
			if (m_timer.elapasedTime >= m_deley)
				GameSceneManager.instance.MoveNextScene(m_sceneIndex);
		}
	}
}
