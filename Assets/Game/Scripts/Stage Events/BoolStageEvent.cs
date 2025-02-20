public class BoolStageEvent : StageEvent
{
	private readonly bool @bool;

	public BoolStageEvent(StageEventType stageEventType, bool @bool) : base(stageEventType)
	{
		this.@bool = @bool;
	}

	public bool GetBoolValue() => @bool;
}