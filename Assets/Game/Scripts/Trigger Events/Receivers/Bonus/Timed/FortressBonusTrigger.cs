using UnityEngine;

public class FortressBonusTrigger : TimedBonusTriggerEventsReceiver
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

	private void Awake()
	{
		nukeFortressField = FindAnyObjectByType<NukeFortressField>();
	}
}