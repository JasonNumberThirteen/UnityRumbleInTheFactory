using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
	public UnityEvent onSelect, onSubmit;

	public OptionType GetOptionType() => optionType;

	public void Select()
	{
		onSelect?.Invoke();
	}

	public void Submit()
	{
		onSubmit?.Invoke();
	}

	[SerializeField] private OptionType optionType;
}