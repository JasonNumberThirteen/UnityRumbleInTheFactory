using UnityEngine;

public class Nuke : MonoBehaviour, ITriggerable
{
	public string destroyedStateLayer;
	
	public void TriggerEffect(GameObject sender)
	{
		ChangeLayer();
		ChangeRendererSprite();
		Explode();
		StageManager.instance.InterruptGame();
	}

	private void ChangeLayer() => gameObject.layer = LayerMask.NameToLayer(destroyedStateLayer);

	private void ChangeRendererSprite()
	{
		if(TryGetComponent(out NukeRenderer nr))
		{
			nr.ChangeToDestroyedState();
		}
	}

	private void Explode()
	{
		if(TryGetComponent(out EntityExploder ee))
		{
			ee.Explode();
		}
	}
}