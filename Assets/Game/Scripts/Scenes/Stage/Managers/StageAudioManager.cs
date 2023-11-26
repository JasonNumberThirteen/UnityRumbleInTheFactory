using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
	public AudioClip playerRobotBulletHit, enemyRobotExplosion, bonusCollect;

	private AudioSource audioSource;

	public void PlayPlayerRobotBulletHitSound() => PlaySound(playerRobotBulletHit);
	public void PlayEnemyRobotExplosionSound() => PlaySound(enemyRobotExplosion);
	public void PlayBonusCollectSound() => PlaySound(bonusCollect);

	private void Awake() => audioSource = GetComponent<AudioSource>();

	private void PlaySound(AudioClip audioClip)
	{
		if(audioClip != null)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}
}