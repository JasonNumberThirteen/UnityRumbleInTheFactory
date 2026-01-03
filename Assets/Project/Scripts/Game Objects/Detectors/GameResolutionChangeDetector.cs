using UnityEngine;
using UnityEngine.Events;

public class GameResolutionChangeDetector : MonoBehaviour
{
	public UnityEvent resolutionWasChangedEvent;

	private int currentScreenWidth;
	private int currentScreenHeight;

	private void Awake()
	{
		UpdateValues();
	}

	private void Update()
	{
		if(!ResolutionWasChanged())
		{
			return;
		}

		UpdateValues();
		resolutionWasChangedEvent?.Invoke();
	}

	private void UpdateValues()
	{
		currentScreenWidth = Screen.width;
		currentScreenHeight = Screen.height;
	}

	private bool ResolutionWasChanged() => Screen.width != currentScreenWidth || Screen.height != currentScreenHeight;
}