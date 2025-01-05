using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
	public UnityEvent optionSelectedEvent;
	public UnityEvent optionSubmittedEvent;

	[SerializeField] private OptionType optionType;

	public OptionType GetOptionType() => optionType;

	public void Select()
	{
		optionSelectedEvent?.Invoke();
	}

	public void Submit()
	{
		optionSubmittedEvent?.Invoke();
	}
}