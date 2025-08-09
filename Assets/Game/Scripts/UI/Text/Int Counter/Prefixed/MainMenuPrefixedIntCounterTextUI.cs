using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MainMenuPrefixedIntCounterTextUI : PrefixedIntCounterTextUI
{
	private RectTransform rectTransform;
	
	public override string GetFormattedCounterValueAsString() => GetCounterValue() > 0 ? base.GetFormattedCounterValueAsString() : "00";

	protected override void Awake()
	{
		base.Awake();

		rectTransform = GetComponent<RectTransform>();
	}

	protected override void OnValueWasChanged()
	{
		base.OnValueWasChanged();
		AdjustWidth();
	}

	private void AdjustWidth()
	{
		var totalMinimumLength = GetHeader().Length + GetPostfix().Length + GetWidth();
		var numberOfCharactersInLine = Mathf.Max(totalMinimumLength, text.text.Length);
		
		rectTransform.sizeDelta = new Vector2(8*numberOfCharactersInLine, rectTransform.sizeDelta.y);
	}
}