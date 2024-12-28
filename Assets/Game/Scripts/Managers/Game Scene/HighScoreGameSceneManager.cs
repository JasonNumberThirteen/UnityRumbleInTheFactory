using UnityEngine;

[RequireComponent(typeof(Timer))]
public class HighScoreGameSceneManager : GameSceneManager
{
	[SerializeField] private AudioSource audioSource;
	
	private Timer timer;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		
		AdjustTimerDurationForMusicLength();
		RegisterToListeners(true);
	}

	private void AdjustTimerDurationForMusicLength()
	{
		if(audioSource != null && audioSource.clip != null)
		{
			timer.duration = audioSource.clip.length;
		}
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}
	
	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerEnd()
	{
		LoadSceneByName(MAIN_MENU_SCENE_NAME);
	}
}