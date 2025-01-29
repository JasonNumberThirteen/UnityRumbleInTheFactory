using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreSoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip pointsCountSound;
	[SerializeField] private AudioClip bonusPointsAwardSound;
	[SerializeField] private AudioClip playerRobotLifeGainSound;
	
	private AudioSource audioSource;
	private ScoreEnemyRobotTypeCountManager scoreEnemyRobotTypeCountManager;
	private ScoreBonusPointsAwardManager scoreBonusPointsAwardManager;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		scoreEnemyRobotTypeCountManager = ObjectMethods.FindComponentOfType<ScoreEnemyRobotTypeCountManager>();
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
			if(scoreEnemyRobotTypeCountManager != null)
			{
				scoreEnemyRobotTypeCountManager.enemyRobotCountedEvent.AddListener(OnEnemyRobotCounted);
			}

			if(scoreBonusPointsAwardManager != null)
			{
				scoreBonusPointsAwardManager.playerAwardedWithPointsEvent.AddListener(OnPlayerAwardedWithPoints);
			}
		}
		else
		{
			if(scoreEnemyRobotTypeCountManager != null)
			{
				scoreEnemyRobotTypeCountManager.enemyRobotCountedEvent.RemoveListener(OnEnemyRobotCounted);
			}

			if(scoreBonusPointsAwardManager != null)
			{
				scoreBonusPointsAwardManager.playerAwardedWithPointsEvent.RemoveListener(OnPlayerAwardedWithPoints);
			}
		}
	}

	private void OnEnemyRobotCounted(List<PlayerRobotScoreData> playerRobotScoreDataList)
	{
		PlaySound(SoundEffectType.PointsCount);
	}

	private void OnPlayerAwardedWithPoints(bool gainedNewLife)
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