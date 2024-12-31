using UnityEngine;

public class RobotAudioSource : MonoBehaviour
{
	public AudioClip shoot, damage;
	
	private AudioSource audioSource;
	private StageSoundManager stageSoundManager;
	
	public void PlayShootSound() => PlaySound(shoot);
	public void PlayDamageSound() => PlaySound(damage);

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
	}

	private void PlaySound(AudioClip audioClip)
	{
		if(stageSoundManager == null || !stageSoundManager.gameObject.activeSelf || audioClip == null)
		{
			return;
		}
		
		audioSource.clip = audioClip;

		audioSource.Play();
	}
}