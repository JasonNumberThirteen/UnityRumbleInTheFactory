using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EntityExploder), typeof(Collider2D))]
public class NukeEntity : MonoBehaviour, ITriggerableOnEnter
{
	public UnityEvent nukeDestroyedEvent;

	private EntityExploder entityExploder;
	private Collider2D c2D;

	private readonly string DESTROYED_STATE_LAYER_NAME = "Destroyed Nuke";

	public bool OverlapPoint(Vector2 point) => c2D.OverlapPoint(point);
	
	public void TriggerOnEnter(GameObject sender)
	{
		gameObject.layer = LayerMask.NameToLayer(DESTROYED_STATE_LAYER_NAME);

		entityExploder.TriggerExplosion();
		nukeDestroyedEvent?.Invoke();
	}

	private void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		c2D = GetComponent<Collider2D>();
	}
}