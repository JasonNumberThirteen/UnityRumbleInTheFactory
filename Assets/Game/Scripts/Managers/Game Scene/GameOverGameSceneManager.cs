using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameOverGameSceneManager : GameSceneManager
{
	[SerializeField] private GameData gameData;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		
		StartCoroutine(LoadNextScene());
	}

	private IEnumerator LoadNextScene()
	{
		yield return new WaitUntil(() => !audioSource.isPlaying);
		
		var sceneName = GameDataMethods.BeatenHighScore(gameData) ? HIGH_SCORE_SCENE_NAME : MAIN_MENU_SCENE_NAME;

		LoadSceneByName(sceneName);
	}
}