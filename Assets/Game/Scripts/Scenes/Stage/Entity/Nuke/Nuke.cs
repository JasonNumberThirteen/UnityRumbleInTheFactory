using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityExploder))]
public class Nuke : MonoBehaviour, ITriggerable
{
	public UnityEvent nukeDestroyedEvent;

	private EntityExploder entityExploder;

	private readonly string DESTROYED_STATE_LAYER = "Destroyed Nuke";
	
	public void TriggerEffect(GameObject sender)
	{
		ChangeLayerToDestroyedState();
		ChangeRendererSprite();
		entityExploder.Explode();
		StageManager.instance.InterruptGame();
		nukeDestroyedEvent?.Invoke();
	}

	private void ChangeLayerToDestroyedState()
	{
		gameObject.layer = LayerMask.NameToLayer(DESTROYED_STATE_LAYER);
	}

	private void ChangeRendererSprite()
	{
		if(TryGetComponent(out NukeRenderer nukeRenderer))
		{
			nukeRenderer.ChangeToDestroyedState();
		}
	}

	private void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
	}
}