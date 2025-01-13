using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class StageMusicManager : MonoBehaviour
{
	public UnityEvent musicStoppedPlayingEvent;
	
	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		
		StartCoroutine(InvokeMusicStoppedPlayingEvent());
	}

	private IEnumerator InvokeMusicStoppedPlayingEvent()
	{
		yield return new WaitUntil(() => !audioSource.isPlaying);

		musicStoppedPlayingEvent?.Invoke();
	}
}