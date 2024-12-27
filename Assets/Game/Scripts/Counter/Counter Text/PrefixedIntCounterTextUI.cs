using UnityEngine;

public class PrefixedIntCounterTextUI : IntCounterTextUI
{
	[SerializeField] private int width;

	public override string GetCounterValueAsString()
	{
		return string.Format("{0," + width + "}", GetFormattedCounterValueAsString());
	}

	public virtual string GetFormattedCounterValueAsString() => base.GetCounterValueAsString();
}