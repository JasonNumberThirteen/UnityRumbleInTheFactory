using UnityEngine;

public class RobotAudioSource : MonoBehaviour
{
	public AudioClip damage;
	
	private AudioSource audioSource;

	public void PlayDamageSound() => PlaySound(damage);

	private void Awake() => audioSource = GetComponent<AudioSource>();

	private void PlaySound(AudioClip audioClip)
	{
		audioSource.clip = audioClip;

		audioSource.Play();
	}
}