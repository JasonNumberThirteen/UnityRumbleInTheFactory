using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
	public AudioClip playerRobotBulletHit, enemyRobotExplosion, bonusCollect;

	private AudioSource[] audioSources;

	public void PlayPlayerRobotBulletHitSound() => PlaySound(playerRobotBulletHit);
	public void PlayEnemyRobotExplosionSound() => PlaySound(enemyRobotExplosion);
	public void PlayBonusCollectSound() => PlaySound(bonusCollect);

	private void Awake() => audioSources = GetComponentsInChildren<AudioSource>();

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