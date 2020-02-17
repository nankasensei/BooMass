using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
	public static GameSceneManager instance { get; private set; } = null;
	static public readonly int numMainScenes = 2;
	static public readonly bool isDeathMoveNextScene = true;

	public GameObject playerObject { get; private set; } = null;
	public int numAllBomb { get; private set; } = 0;
	public int numPlayerHaveBomb { get { return m_playerExplosionManagement? m_playerExplosionManagement.haveFireIndex + 1 : 0; } }
	public float playerExplosionLimit { get { return m_playerExplosionManagement ? m_playerExplosionManagement.explosionLimitTime : 0.0f; } }
	public float playerExplosionElapased { get { return m_playerExplosionManagement ? m_playerExplosionManagement.explosionElapasedTime : 0.0f; } }
	public bool isPlayerAllHaveBoms { get { return m_playerExplosionManagement ? 
				m_playerExplosionManagement.haveFireIndex + 1 == numAllBomb : false; } }
	public bool isPlayerDeath { get {
			return m_playerExplosionManagement ?
			m_playerExplosionManagement.isPlayerDeath : false; } }

	public System.Action gameNotClearCallback { get; set; } = null;
	public System.Action gameClearCallback { get; set; } = null;
	public bool isGameClear { get; private set; } = false;

	public System.Action gameOverCallback { get; set; } = null;
	public bool isGameOver { get; private set; } = false;

	public System.Action gameOverOrClearCallback { get; set; } = null;
	public bool isGameOverOrClear { get { return isGameClear | isGameOver; } }

	public System.Action gameStartCallback { get; set; } = null;
	public System.Action gameEndCallback { get; set; } = null;
	public bool isInGame { get; private set; } = false;

	private PlayerExplosionManagement m_playerExplosionManagement = null;

	//GameOver, シーン遷移, 現在のシーン

	public void MoveNextScene(int ifMainSceneNumber = -1)
	{
		if (isDeathMoveNextScene) SceneLoader.LoadNextScene(ifMainSceneNumber);
		else SceneLoader.ReloadScene();
	}
	public void ReloadScene()
	{
		SceneLoader.ReloadScene();
	}

	public void GameOver()
	{
		isGameOver = true;
		gameOverCallback?.Invoke();
		gameOverOrClearCallback?.Invoke();
	}
	public void GameClear()
	{
		isGameClear = true;
		gameClearCallback?.Invoke();
		gameOverOrClearCallback?.Invoke();
	}

	public void IncrementNumAllBomb()
	{
		++numAllBomb;
	}
	public void SetPlayerObject(GameObject set)
	{
		playerObject = set;
	}
	public void SetPlayerExplosionManagement(PlayerExplosionManagement set)
	{
		m_playerExplosionManagement = set;
	}

	public void GameStart()
	{
		isInGame = true;
		gameStartCallback?.Invoke();
	}
	public void GameEnd()
	{
		isInGame = false;
		gameEndCallback?.Invoke();
	}

	void Awake()
	{
		instance = this;
		GoalPoint.gameNotClearCallback += Invoke;
		TakeCollision.callback = null;
	}
	void OnDestroy()
	{
		instance = null;
	}	
	void Invoke()
	{
		gameNotClearCallback?.Invoke();
	}
}
