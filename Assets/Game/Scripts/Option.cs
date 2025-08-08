using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class Option : MonoBehaviour
{
	public UnityEvent<Option> optionWasSelectedEvent;
	public UnityEvent<Option> optionWasSubmittedEvent;

	[SerializeField] private OptionType optionType;

	private RectTransform rectTransform;

	public OptionType GetOptionType() => optionType;
	public Vector2 GetPosition() => rectTransform.anchoredPosition;

	public void Select()
	{
		optionWasSelectedEvent?.Invoke(this);
	}

	public void Submit()
	{
		optionWasSubmittedEvent?.Invoke(this);
	}

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}
}