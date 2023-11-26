using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
	public AudioClip playerRobotBulletHit, enemyRobotExplosion, bonusCollect;

	private AudioSource audioSource;

	public void PlaySound(AudioClip audioClip) => audioSource.PlayOneShot(audioClip);

	private void Awake() => audioSource = GetComponent<AudioSource>();
}