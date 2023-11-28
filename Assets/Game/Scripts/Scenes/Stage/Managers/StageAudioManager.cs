using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
	public AudioClip playerRobotIdle, playerRobotMovement, playerRobotBulletHit, enemyRobotExplosion, bonusCollect;

	private AudioSource[] audioSources;
	private AudioSource playerRobotMovementChannel;
	private float playerRobotMovementChannelVolume;

	public void StopPlayerRobotMovementChannel() => playerRobotMovementChannel.Stop();
	public void PlayPlayerRobotBulletHitSound() => PlaySound(playerRobotBulletHit);
	public void PlayEnemyRobotExplosionSound() => PlaySound(enemyRobotExplosion);
	public void PlayBonusCollectSound()
	{
		PlaySound(bonusCollect);
		MutePlayerRobotMovementChannelTemporarily(1);
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
		if(playerRobotMovementChannel.clip == playerRobotIdle)
		{
			return;
		}
		
		playerRobotMovementChannel.clip = playerRobotIdle;

		playerRobotMovementChannel.Play();
	}

	public void PlayPlayerRobotMovementSound()
	{
		if(playerRobotMovementChannel.clip == playerRobotMovement)
		{
			return;
		}
		
		playerRobotMovementChannel.clip = playerRobotMovement;

		playerRobotMovementChannel.Play();
	}

	private void RestorePlayerRobotMovementChannelVolume() => playerRobotMovementChannel.volume = playerRobotMovementChannelVolume;

	private void Awake()
	{
		audioSources = GetComponentsInChildren<AudioSource>();
		playerRobotMovementChannel = audioSources[0];
		playerRobotMovementChannelVolume = playerRobotMovementChannel.volume;
	}

	private void PlaySound(AudioClip audioClip)
	{
		if(audioClip != null)
		{
			AudioSource freeAudioSource = null;

			for (int i = 0; i < audioSources.Length && freeAudioSource == null; ++i)
			{
				AudioSource current = audioSources[i];

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
}