using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityExploder))]
public class Nuke : MonoBehaviour, ITriggerableOnEnter
{
	public UnityEvent nukeDestroyedEvent;

	private EntityExploder entityExploder;

	private readonly string DESTROYED_STATE_LAYER = "Destroyed Nuke";
	
	public void TriggerOnEnter(GameObject sender)
	{
		ChangeLayerToDestroyedState();
		entityExploder.Explode();
		nukeDestroyedEvent?.Invoke();
	}

	private void ChangeLayerToDestroyedState()
	{
		gameObject.layer = LayerMask.NameToLayer(DESTROYED_STATE_LAYER);
	}

	private void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
	}
}