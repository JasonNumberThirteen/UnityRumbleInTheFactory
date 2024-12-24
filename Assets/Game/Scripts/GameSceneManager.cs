using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
	public void LoadSceneByName(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}