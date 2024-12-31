using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class StageSoundManager : MonoBehaviour
{
	public UnityEvent<SoundEffectType> soundPlayedEvent;
	
	[SerializeField, Range(0, 3)] private int additionalSoundChannels = 3;
	[SerializeField] private AudioClip playerRobotIdleSound;
	[SerializeField] private AudioClip playerRobotMovementSound;
	[SerializeField] private AudioClip playerRobotBulletHitSound;
	[SerializeField] private AudioClip enemyRobotExplosionSound;
	[SerializeField] private AudioClip bonusSpawnSound;
	[SerializeField] private AudioClip bonusCollectSound;

	private readonly List<AudioSource> audioSources = new();
	private PlayerRobotMovementSoundChannel playerRobotMovementSourceChannel;
	private StageMusicManager stageMusicManager;

	private void Awake()
	{
		playerRobotMovementSourceChannel = GetComponentInChildren<PlayerRobotMovementSoundChannel>();
		stageMusicManager = FindAnyObjectByType<StageMusicManager>();

		RegisterToListeners(true);
	}

	private void Start()
	{
		for (var i = 0; i < additionalSoundChannels; ++i)
		{
			audioSources.Add(gameObject.AddComponent<AudioSource>());
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
			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.AddListener(OnMusicStoppedPlaying);
			}
		}
		else
		{
			if(stageMusicManager != null)
			{
				stageMusicManager.musicStoppedPlayingEvent.RemoveListener(OnMusicStoppedPlaying);
			}
		}
	}

	private void OnMusicStoppedPlaying()
	{
		StageManager.instance.EnableAudioManager();
	}

	public void PlaySound(SoundEffectType soundEffectType)
	{
		var audioClip = GetAudioClipBySoundEffectType(soundEffectType);

		if(audioClip == null)
		{
			return;
		}

		if(soundEffectType == SoundEffectType.PlayerRobotIdle || soundEffectType == SoundEffectType.PlayerRobotMovement)
		{
			playerRobotMovementSourceChannel.Play(audioClip);
			soundPlayedEvent?.Invoke(soundEffectType);
		}
		else
		{
			var freeAudioSource = audioSources.FirstOrDefault(audioSource => !audioSource.isPlaying);

			if(freeAudioSource != null)
			{
				freeAudioSource.PlayOneShot(audioClip);
				soundPlayedEvent?.Invoke(soundEffectType);
			}
		}
	}

	private AudioClip GetAudioClipBySoundEffectType(SoundEffectType soundEffectType)
	{
		return soundEffectType switch
		{
			SoundEffectType.PlayerRobotIdle => playerRobotIdleSound,
			SoundEffectType.PlayerRobotMovement => playerRobotMovementSound,
			SoundEffectType.PlayerRobotBulletHit => playerRobotBulletHitSound,
			SoundEffectType.EnemyRobotExplosion => enemyRobotExplosionSound,
			SoundEffectType.BonusSpawn => bonusSpawnSound,
			SoundEffectType.BonusCollect => bonusCollectSound,
			_ => null
		};
	}
}