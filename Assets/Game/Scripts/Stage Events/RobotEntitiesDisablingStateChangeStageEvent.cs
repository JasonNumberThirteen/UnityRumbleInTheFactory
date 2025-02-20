using System.Linq;

public class RobotEntitiesDisablingStageEvent : StageEvent
{
	private readonly bool disablingIsActive;
	private readonly RobotEntity[] robotEntities;

	public RobotEntitiesDisablingStageEvent(StageEventType stageEventType, bool disablingIsActive, RobotEntity[] robotEntities) : base(stageEventType)
	{
		this.disablingIsActive = disablingIsActive;
		this.robotEntities = robotEntities;
	}

	public bool DisablingIsActive() => disablingIsActive;
	public bool AppliesTo(RobotEntity robotEntity) => robotEntities.Contains(robotEntity);
}