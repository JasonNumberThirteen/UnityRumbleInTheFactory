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
	private AudioSource playerRobotMovementChannel;
	private float playerRobotMovementChannelVolume;
	private StageMusicManager stageMusicManager;

	public void StopPlayerRobotMovementChannel()
	{
		playerRobotMovementChannel.Stop();
	}

	public void PlayBonusSpawnSound()
	{
		PlaySound(SoundEffectType.BonusSpawn);
		MutePlayerRobotMovementChannelTemporarily(1);
	}

	public void PlayBonusCollectSound()
	{
		PlaySound(SoundEffectType.BonusCollect);
		MutePlayerRobotMovementChannelTemporarily(0.9f);
	}

	public void MutePlayerRobotMovementChannelTemporarily(float duration)
	{
		playerRobotMovementChannel.volume = 0;

		Invoke(nameof(RestorePlayerRobotMovementChannelVolume), duration);
	}

	public void SwitchPlayerRobotMovementChannel()
	{
		if(playerRobotMovementChannel.isPlaying)
		{
			playerRobotMovementChannel.Pause();
		}
		else
		{
			playerRobotMovementChannel.UnPause();
		}
	}

	public void PlayPlayerRobotIdleSound()
	{
		if(playerRobotMovementChannel.clip == playerRobotIdleSound)
		{
			return;
		}
		
		playerRobotMovementChannel.clip = playerRobotIdleSound;

		playerRobotMovementChannel.Play();
	}

	public void PlayPlayerRobotMovementSound()
	{
		if(playerRobotMovementChannel.clip == playerRobotMovementSound)
		{
			return;
		}
		
		playerRobotMovementChannel.clip = playerRobotMovementSound;

		playerRobotMovementChannel.Play();
	}

	private void RestorePlayerRobotMovementChannelVolume()
	{
		playerRobotMovementChannel.volume = playerRobotMovementChannelVolume;
	}

	private void Awake()
	{
		audioSources = GetComponentsInChildren<AudioSource>();
		playerRobotMovementChannel = audioSources[0];
		playerRobotMovementChannelVolume = playerRobotMovementChannel.volume;
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