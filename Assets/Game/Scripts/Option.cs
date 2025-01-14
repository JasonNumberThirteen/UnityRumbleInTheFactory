using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class Option : MonoBehaviour
{
	public UnityEvent<Option> optionSelectedEvent;
	public UnityEvent<Option> optionSubmittedEvent;

	[SerializeField] private OptionType optionType;

	private RectTransform rectTransform;

	public OptionType GetOptionType() => optionType;
	public Vector2 GetPosition() => rectTransform.anchoredPosition;

	public void Select()
	{
		optionSelectedEvent?.Invoke(this);
	}

	public void Submit()
	{
		optionSubmittedEvent?.Invoke(this);
	}

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}
}