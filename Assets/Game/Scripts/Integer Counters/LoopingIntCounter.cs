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
			SetTo(Mathf.Clamp(CurrentValue, this.lowerBound, this.upperBound));
		}
	}

	protected override void IncreaseValue(int value)
	{
		SetTo(GetNextValue(value));
	}

	protected override void DecreaseValue(int value)
	{
		SetTo(GetPreviousValue(value));
	}

	private int GetNextValue(int value) => (CurrentValue + value - 1) % upperBound + lowerBound;
	private int GetPreviousValue(int value) => (CurrentValue - value + upperBound - 1) % upperBound + lowerBound;
}