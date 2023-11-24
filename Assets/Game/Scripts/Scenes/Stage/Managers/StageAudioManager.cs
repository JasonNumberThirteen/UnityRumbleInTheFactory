using UnityEngine;

public class StageAudioManager : MonoBehaviour
{
	public AudioClip enemyRobotExplosion;

	private AudioSource audioSource;

	public void PlaySound(AudioClip audioClip) => audioSource.PlayOneShot(audioClip);

	private void Awake() => audioSource = GetComponent<AudioSource>();
}