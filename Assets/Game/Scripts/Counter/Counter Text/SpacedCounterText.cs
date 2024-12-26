public class SpacedCounterText : IntCounterTextUI
{
	public int width;

	public override string FormattedCounterValue() => string.Format("{0," + width + "}", base.FormattedCounterValue());
}