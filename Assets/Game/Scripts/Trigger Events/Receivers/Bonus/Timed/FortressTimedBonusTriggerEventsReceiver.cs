using UnityEngine;

public class FortressTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private NukeEntityFortressField nukeEntityFortressField;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(nukeEntityFortressField != null)
		{
			nukeEntityFortressField.BuildFortress(GetDuration());
		}
		
		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();
		
		nukeEntityFortressField = FindAnyObjectByType<NukeEntityFortressField>();
	}
}