using UnityEngine;

public class PrefixedIntCounterTextUI : IntCounterTextUI
{
	[SerializeField, Min(0)] private int width;

	public override string GetCounterValueAsString() => string.Format("{0," + width + "}", GetFormattedCounterValueAsString());

	public virtual string GetFormattedCounterValueAsString() => base.GetCounterValueAsString();

	public int GetWidth() => width;
}