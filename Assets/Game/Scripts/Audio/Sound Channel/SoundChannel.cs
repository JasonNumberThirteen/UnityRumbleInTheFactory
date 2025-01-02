using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundChannel : MonoBehaviour
{
	protected AudioSource audioSource;

	public bool SoundIsPlaying() => audioSource.isPlaying;

	public virtual void Play(AudioClip audioClip)
	{
		if(audioClip != null)
		{
			audioSource.PlayOneShot(audioClip);
		}
	}

	protected virtual void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
}