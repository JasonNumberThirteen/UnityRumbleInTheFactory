using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreSoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip pointsCountSound;
	[SerializeField] private AudioClip bonusPointsAwardSound;
	[SerializeField] private AudioClip playerRobotLifeGainSound;
	
	private AudioSource audioSource;
	private ScoreEnemyTypeCountManager scoreEnemyTypeCountManager;
	private ScoreBonusPointsAwardManager scoreBonusPointsAwardManager;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		scoreEnemyTypeCountManager = ObjectMethods.FindComponentOfType<ScoreEnemyTypeCountManager>();
		scoreBonusPointsAwardManager = ObjectMethods.FindComponentOfType<ScoreBonusPointsAwardManager>(false);

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			if(scoreEnemyTypeCountManager != null)
			{
				scoreEnemyTypeCountManager.enemyWasCountedEvent.AddListener(OnEnemyWasCounted);
			}

			if(scoreBonusPointsAwardManager != null)
			{
				scoreBonusPointsAwardManager.playerWasAwardedWithPointsEvent.AddListener(OnPlayerWasAwardedWithPoints);
			}
		}
		else
		{
			if(scoreEnemyTypeCountManager != null)
			{
				scoreEnemyTypeCountManager.enemyWasCountedEvent.RemoveListener(OnEnemyWasCounted);
			}

			if(scoreBonusPointsAwardManager != null)
			{
				scoreBonusPointsAwardManager.playerWasAwardedWithPointsEvent.RemoveListener(OnPlayerWasAwardedWithPoints);
			}
		}
	}

	private void OnEnemyWasCounted(List<PlayerRobotScoreData> playerRobotScoreDataList)
	{
		PlaySound(SoundEffectType.PointsCount);
	}

	private void OnPlayerWasAwardedWithPoints(bool gainedNewLife)
	{
		PlaySound(gainedNewLife ? SoundEffectType.PlayerRobotLifeGain : SoundEffectType.BonusPointsAward);
	}

	private void PlaySound(SoundEffectType soundEffectType)
	{
		var audioClip = GetAudioClipBySoundEffectType(soundEffectType);

		if(audioClip != null)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}

	private AudioClip GetAudioClipBySoundEffectType(SoundEffectType soundEffectType)
	{
		return soundEffectType switch
		{
			SoundEffectType.PointsCount => pointsCountSound,
			SoundEffectType.BonusPointsAward => bonusPointsAwardSound,
			SoundEffectType.PlayerRobotLifeGain => playerRobotLifeGainSound,
			_ => null
		};
	}
}