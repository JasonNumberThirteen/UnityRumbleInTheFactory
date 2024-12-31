using UnityEngine;

public class StageSoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip playerRobotIdleSound;
	[SerializeField] private AudioClip playerRobotMovementSound;
	[SerializeField] private AudioClip playerRobotBulletHitSound;
	[SerializeField] private AudioClip enemyRobotExplosionSound;
	[SerializeField] private AudioClip bonusSpawnSound;
	[SerializeField] private AudioClip bonusCollectSound;

	private AudioSource[] audioSources;
	private PlayerRobotMovementSoundChannel playerRobotMovementSourceChannel;
	private StageMusicManager stageMusicManager;

	public void PlayBonusSpawnSound()
	{
		PlaySound(SoundEffectType.BonusSpawn);
		playerRobotMovementSourceChannel.MuteTemporarily(1);
	}

	public void PlayBonusCollectSound()
	{
		PlaySound(SoundEffectType.BonusCollect);
		playerRobotMovementSourceChannel.MuteTemporarily(0.9f);
	}

	private void Awake()
	{
		audioSources = GetComponentsInChildren<AudioSource>();
		playerRobotMovementSourceChannel = GetComponentInChildren<PlayerRobotMovementSoundChannel>();
		stageMusicManager = FindAnyObjectByType<StageMusicManager>();

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
		
		if(audioClip != null)
		{
			if(soundEffectType == SoundEffectType.PlayerRobotIdle || soundEffectType == SoundEffectType.PlayerRobotMovement)
			{
				playerRobotMovementSourceChannel.Play(audioClip);
				return;
			}
			
			AudioSource freeAudioSource = null;

			for (int i = 0; i < audioSources.Length && freeAudioSource == null; ++i)
			{
				var current = audioSources[i];

				if(!current.isPlaying)
				{
					freeAudioSource = current;
				}
			}
			
			if(freeAudioSource != null)
			{
				freeAudioSource.PlayOneShot(audioClip);
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