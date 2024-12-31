using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundChannel : MonoBehaviour
{
	protected AudioSource audioSource;

	public virtual void Play(AudioClip audioClip)
	{
		audioSource.PlayOneShot(audioClip);
	}

	protected virtual void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}
}