public class BonusPointsCounterPanelUI : CounterPanelUI
{
	public void Setup(int numberOfPoints)
	{
		if(intCounter != null)
		{
			intCounter.SetTo(numberOfPoints);
		}
	}
}