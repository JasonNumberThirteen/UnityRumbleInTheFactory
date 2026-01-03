public class StageEvent
{
	private readonly StageEventType stageEventType;

	public StageEvent(StageEventType stageEventType)
	{
		this.stageEventType = stageEventType;
	}

	public StageEventType GetStageEventType() => stageEventType;
}