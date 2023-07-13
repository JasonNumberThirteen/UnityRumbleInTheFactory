using UnityEngine;

public class NukeTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect()
	{
		NukeRenderer nr = GetComponent<NukeRenderer>();
		EntityExploder ee = GetComponent<EntityExploder>();

		if(nr != null)
		{
			nr.ChangeToDestroyedState();
			Destroy(nr);
		}
		
		if(ee != null)
		{
			ee.Explode();
		}

		StageManager.instance.SetGameAsOver();
	}
}