using UnityEngine;

public class FortressTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private NukeEntityFortressField nukeEntityFortressField;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(nukeEntityFortressField != null && sender.TryGetComponent(out RobotEntity robotEntity))
		{
			if(robotEntity.IsFriendly())
			{
				nukeEntityFortressField.SpawnFortress(GetDuration());
			}
			else
			{
				nukeEntityFortressField.DestroyAllGOsWithinArea();
			}
		}
		
		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();
		
		nukeEntityFortressField = FindAnyObjectByType<NukeEntityFortressField>();
	}
}