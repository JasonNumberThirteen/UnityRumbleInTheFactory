using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
	public void LoadScene(string name) => SceneManager.LoadScene(name);
}