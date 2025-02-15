using UnityEngine;

public class LoopingIntCounter : IntCounter
{
	private int lowerBound;
	private int upperBound;

	public void SetRange(int lowerBound, int upperBound)
	{
		if(lowerBound > upperBound)
		{
			return;
		}
		
		this.lowerBound = lowerBound;
		this.upperBound = upperBound;

		SetTo(CurrentValue);
	}

	public override void ModifyBy(int value)
	{
		var newValue = value >= 0 ? GetNextValue(value) : GetPreviousValue(Mathf.Abs(value));

		SetTo(newValue);
	}

	protected override int GetModifiedValue(int value) => value.GetClampedValue(lowerBound, upperBound);
	
	private int GetNextValue(int value) => GetClampedValue(CurrentValue + value - 1);
	private int GetPreviousValue(int value) => GetClampedValue(CurrentValue - value + upperBound - 1);
	private int GetClampedValue(int value) => value % upperBound + lowerBound;
}