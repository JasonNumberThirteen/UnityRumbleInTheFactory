public class SpacedCounterText : IntCounterTextUI
{
	public int width;

	public override string GetFormattedCounterValue() => string.Format("{0," + width + "}", base.GetFormattedCounterValue());
}