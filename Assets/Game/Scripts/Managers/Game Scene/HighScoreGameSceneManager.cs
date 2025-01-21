using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HighScoreGameSceneManager : GameSceneManager
{
	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		
		StartCoroutine(LoadNextScene());
	}

	private IEnumerator LoadNextScene()
	{
		yield return new WaitUntil(() => !audioSource.isPlaying);
		
		LoadSceneByName(MAIN_MENU_SCENE_NAME);
	}
}