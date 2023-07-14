using UnityEngine;

public class NukeTrigger : MonoBehaviour, ITriggerable
{
	public string destroyedStateLayer;
	
	public void TriggerEffect(GameObject sender)
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

		gameObject.layer = LayerMask.NameToLayer(destroyedStateLayer);

		StageManager.instance.SetGameAsOver();
	}
}