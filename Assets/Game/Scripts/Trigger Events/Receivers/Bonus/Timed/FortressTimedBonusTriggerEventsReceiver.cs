using UnityEngine;

public class FortressTimedBonusTriggerEventsReceiver : TimedBonusTriggerEventsReceiver
{
	private NukeFortressField nukeFortressField;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		if(nukeFortressField != null)
		{
			nukeFortressField.BuildFortress(GetDuration());
		}
		
		base.TriggerOnEnter(sender);
	}

	protected override void Awake()
	{
		base.Awake();
		
		nukeFortressField = FindAnyObjectByType<NukeFortressField>();
	}
}