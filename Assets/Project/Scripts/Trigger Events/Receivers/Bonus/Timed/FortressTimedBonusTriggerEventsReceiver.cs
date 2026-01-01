using UnityEngine;

public class FortressTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private NukeEntityFortressField nukeEntityFortressField;
	
	protected override void GiveEffect(GameObject sender)
	{
		if(nukeEntityFortressField == null || !sender.TryGetComponent(out RobotEntity robotEntity))
		{
			return;
		}

		if(robotEntity.IsFriendly())
		{
			nukeEntityFortressField.SpawnFortress(GetDuration());
		}
		else
		{
			nukeEntityFortressField.DestroyAllGOsWithinArea();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		
		nukeEntityFortressField = ObjectMethods.FindComponentOfType<NukeEntityFortressField>();
	}
}