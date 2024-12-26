using UnityEngine;

public class SpacedIntCounterTextUI : IntCounterTextUI
{
	[SerializeField] private int width;

	public override string GetFormattedCounterValue()
	{
		var value = base.GetFormattedCounterValue();
		
		return string.Format("{0," + width + "}", value);
	}
}