public class MainMenuPrefixedIntCounterTextUI : PrefixedIntCounterTextUI
{
	public override string GetFormattedCounterValueAsString() => GetCounterValue() > 0 ? base.GetFormattedCounterValueAsString() : "00";
}