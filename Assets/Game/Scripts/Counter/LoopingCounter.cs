public class LoopingCounter : IntCounter
{
	public int min, max;

	public void SetRange(int min, int max)
	{
		this.min = min;
		this.max = max;
	}

	protected override void IncreaseValue(int value)
	{
		int nextValue = NextValue(value);

		SetTo(nextValue);
	}

	protected override void DecreaseValue(int value)
	{
		int previousValue = PreviousValue(value);

		SetTo(previousValue);
	}

	private int NextValue(int value) => (CurrentValue + value - 1) % max + min;
	private int PreviousValue(int value) => (CurrentValue - value + max - 1) % max + min;
}