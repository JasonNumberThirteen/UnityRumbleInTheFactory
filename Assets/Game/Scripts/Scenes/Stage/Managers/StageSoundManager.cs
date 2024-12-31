using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageSoundManager : MonoBehaviour
{
	public UnityEvent<SoundEffectType> soundPlayedEvent;
	
	[SerializeField, Range(0, 7)] private int additionalSoundChannels = 7;
	[SerializeField] private AudioClip robotDamageSound;
	[SerializeField] private AudioClip playerRobotIdleSound;
	[SerializeField] private AudioClip playerRobotMovementSound;
	[SerializeField] private AudioClip playerRobotShootSound;
	[SerializeField] private AudioClip playerRobotBulletHitSound;
	[SerializeField] private AudioClip enemyRobotExplosionSound;
	[SerializeField] private AudioClip bonusSpawnSound;
	[SerializeField] private AudioClip bonusCollectSound;

	private readonly List<SoundChannel> soundChannels = new();
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
			var childChannel = new GameObject($"Channel {i + 1}");
			var soundChannel = childChannel.AddComponent<SoundChannel>();

			soundChannels.Add(soundChannel);
			childChannel.transform.SetParent(gameObject.transform);
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
		var soundChannel = GetSoundChannelBySoundEffectType(soundEffectType);

		if(soundChannel == null)
		{
			return;
		}

		soundChannel.Play(GetAudioClipBySoundEffectType(soundEffectType));
		soundPlayedEvent?.Invoke(soundEffectType);
	}

	private AudioClip GetAudioClipBySoundEffectType(SoundEffectType soundEffectType)
	{
		return soundEffectType switch
		{
			SoundEffectType.RobotDamage => robotDamageSound,
			SoundEffectType.PlayerRobotIdle => playerRobotIdleSound,
			SoundEffectType.PlayerRobotMovement => playerRobotMovementSound,
			SoundEffectType.PlayerRobotShoot => playerRobotShootSound,
			SoundEffectType.PlayerRobotBulletHit => playerRobotBulletHitSound,
			SoundEffectType.EnemyRobotExplosion => enemyRobotExplosionSound,
			SoundEffectType.BonusSpawn => bonusSpawnSound,
			SoundEffectType.BonusCollect => bonusCollectSound,
			_ => null
		};
	}

	private SoundChannel GetSoundChannelBySoundEffectType(SoundEffectType soundEffectType)
	{
		var playerRobotTypes = new List<SoundEffectType>
		{
			SoundEffectType.PlayerRobotIdle,
			SoundEffectType.PlayerRobotMovement
		};

		return playerRobotTypes.Contains(soundEffectType) ? playerRobotMovementSourceChannel : soundChannels.FirstOrDefault(soundChannel => !soundChannel.SoundIsPlaying());
	}
}