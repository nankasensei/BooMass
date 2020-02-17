using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
	public static readonly string[] sceneList = { "TitleScene", "EndingScene" };
	public static readonly string[] mainSceneList = { "TutorialScene", "MainScene" };
	
	public static void LoadNextScene(int loadNumber = -1)
	{
		int nowIndex = 0;
		string loadName = "";

		if (loadNumber == -1)
		{
			if (sceneList[1] == SceneManager.GetActiveScene().name)
				nowIndex = 0;
			else
				nowIndex = 0;
			
			loadName = sceneList[nowIndex];
		}
		else
		{
			loadName = mainSceneList[loadNumber];
		}

		SceneManager.LoadScene(loadName);
	}

	public static void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
