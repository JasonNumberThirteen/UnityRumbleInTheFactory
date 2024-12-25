using UnityEngine;

[RequireComponent(typeof(Timer), typeof(RectTransformTimedMover))]
public class MainMenuPanelUI : MonoBehaviour
{
	[SerializeField] private GameObject optionsCursorGO;
	
	private Timer timer;
	private RectTransformTimedMover rectTransformTimedMover;

	private void Awake()
	{
		timer = GetComponent<Timer>();
		rectTransformTimedMover = GetComponent<RectTransformTimedMover>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.onEnd.AddListener(ActivateOptionsCursorGO);
			timer.onInterrupt.AddListener(OnTimerInterrupt);
		}
		else
		{
			timer.onEnd.RemoveListener(ActivateOptionsCursorGO);
			timer.onInterrupt.RemoveListener(OnTimerInterrupt);
		}
	}

	private void OnTimerInterrupt()
	{
		rectTransformTimedMover.SetPositionY(0);
		ActivateOptionsCursorGO();
	}

	private void ActivateOptionsCursorGO()
	{
		if(optionsCursorGO != null)
		{
			optionsCursorGO.SetActive(true);
		}
	}
}