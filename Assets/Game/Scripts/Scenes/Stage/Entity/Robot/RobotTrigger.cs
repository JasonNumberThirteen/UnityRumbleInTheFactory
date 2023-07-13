using UnityEngine;

public class RobotTrigger : MonoBehaviour, ITriggerable
{
	public virtual void TriggerEffect()
	{
		EntityExploder ee = GetComponent<EntityExploder>();

		if(ee != null)
		{
			ee.Explode();
		}
	}
}