using UnityEngine;

public class LoopingIntCounter : IntCounter
{
	private int lowerBound;
	private int upperBound;

	public void SetRange(int lowerBound, int upperBound)
	{
		this.lowerBound = lowerBound;
		this.upperBound = upperBound;

		if(CurrentValue < this.lowerBound || CurrentValue > this.upperBound)
		{
			var valueWithinRange = Mathf.Clamp(CurrentValue, this.lowerBound, this.upperBound);
			
			SetTo(valueWithinRange);
		}
	}

	protected override void IncreaseValue(int value)
	{
		var nextValue = GetNextValue(value);

		SetTo(nextValue);
	}

	protected override void DecreaseValue(int value)
	{
		var previousValue = GetPreviousValue(value);

		SetTo(previousValue);
	}

	private int GetNextValue(int value) => (CurrentValue + value - 1) % upperBound + lowerBound;
	private int GetPreviousValue(int value) => (CurrentValue - value + upperBound - 1) % upperBound + lowerBound;
}