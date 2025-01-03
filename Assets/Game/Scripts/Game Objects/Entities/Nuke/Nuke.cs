using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityExploder), typeof(Collider2D))]
public class Nuke : MonoBehaviour, ITriggerableOnEnter
{
	public UnityEvent nukeDestroyedEvent;

	private EntityExploder entityExploder;
	private Collider2D c2D;

	private readonly string DESTROYED_STATE_LAYER = "Destroyed Nuke";

	public bool OverlapPoint(Vector2 point) => c2D.OverlapPoint(point);
	
	public void TriggerOnEnter(GameObject sender)
	{
		ChangeLayerToDestroyedState();
		entityExploder.TriggerExplosion();
		nukeDestroyedEvent?.Invoke();
	}

	private void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		c2D = GetComponent<Collider2D>();
	}

	private void ChangeLayerToDestroyedState()
	{
		gameObject.layer = LayerMask.NameToLayer(DESTROYED_STATE_LAYER);
	}
}