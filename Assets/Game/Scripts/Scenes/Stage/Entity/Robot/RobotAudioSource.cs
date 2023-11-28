using UnityEngine;

public class RobotAudioSource : MonoBehaviour
{
	public AudioClip shoot, damage;
	
	private AudioSource audioSource;
	
	public void PlayShootSound() => PlaySound(shoot);
	public void PlayDamageSound() => PlaySound(damage);

	private void Awake() => audioSource = GetComponent<AudioSource>();

	private void PlaySound(AudioClip audioClip)
	{
		if(audioClip == null || !StageManager.instance.audioManager.gameObject.activeSelf)
		{
			return;
		}
		
		audioSource.clip = audioClip;

		audioSource.Play();
	}
}