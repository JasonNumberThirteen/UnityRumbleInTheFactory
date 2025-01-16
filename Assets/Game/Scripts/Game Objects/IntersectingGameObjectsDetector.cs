using UnityEngine;

public class IntersectingGameObjectsDetector : MonoBehaviour
{
	[SerializeField] private LayerMask gameObjectsLayerMask;
	[SerializeField] private string intersectingGameObjectsLayerName;
	[SerializeField, Min(0.01f)] private float radius = 1f;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color radiusGizmosColor = Color.black;

	private Collider2D[] detectedColliders;

	private readonly int OVERLAP_BUFFER_SIZE = 2;

	public void SetLayerToGOsIfIntersect()
	{
		Physics2D.OverlapBoxNonAlloc(transform.position, Vector2.one*radius, 0f, detectedColliders, gameObjectsLayerMask);

		if(detectedColliders != null && detectedColliders.Length == OVERLAP_BUFFER_SIZE)
		{
			ConfigureGOs();
		}
	}

	private void Awake()
	{
		detectedColliders = new Collider2D[OVERLAP_BUFFER_SIZE];
	}

	private void ConfigureGOs()
	{
		foreach (var collider in detectedColliders)
		{
			if(collider == null)
			{
				continue;
			}
			
			AddTriggerEventsReceiverToGOIfNeeded(collider.gameObject);
			SetLayerToGO(collider.gameObject);
		}
	}

	private void AddTriggerEventsReceiverToGOIfNeeded(GameObject go)
	{
		if(!go.TryGetComponent(out IntersectingGameObjectTriggerEventsReceiver _))
		{
			go.AddComponent<IntersectingGameObjectTriggerEventsReceiver>();
		}
	}

	private void SetLayerToGO(GameObject go)
	{
		go.layer = LayerMask.NameToLayer(intersectingGameObjectsLayerName);
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos)
		{
			GizmosMethods.OperateOnGizmos(() => Gizmos.DrawWireCube(transform.position, Vector3.one*radius), radiusGizmosColor);
		}
	}
}