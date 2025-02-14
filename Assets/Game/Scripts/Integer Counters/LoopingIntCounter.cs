using UnityEngine;

public class LoopingIntCounter : IntCounter
{
	private int lowerBound;
	private int upperBound;

	public void SetRange(int lowerBound, int upperBound)
	{
		this.lowerBound = lowerBound;
		this.upperBound = upperBound;

		SetTo(CurrentValue);
	}

	public override void ModifyBy(int value)
	{
		if(value > 0)
		{
			SetTo(GetNextValue(value));
		}
		else if(value < 0)
		{
			SetTo(GetPreviousValue(Mathf.Abs(value)));
		}
	}

	protected override int GetModifiedValue(int value) => value.GetClampedValue(lowerBound, upperBound);
	private int GetNextValue(int value) => (CurrentValue + value - 1) % upperBound + lowerBound;
	private int GetPreviousValue(int value) => (CurrentValue - value + upperBound - 1) % upperBound + lowerBound;
}