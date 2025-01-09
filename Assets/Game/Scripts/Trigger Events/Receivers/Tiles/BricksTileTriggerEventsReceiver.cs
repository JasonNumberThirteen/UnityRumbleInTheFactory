using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BricksTileTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	[SerializeField] private LayerMask overlapLayerMask;
	
	private Collider2D c2D;
	private Collider2D[] detectedColliders;

	private readonly int OVERLAP_BUFFER_SIZE = 4;
	private readonly float OVERLAP_RANGE = 0.75f;
	private readonly float OVERLAP_BOUNDS_PADDING = 0.05f;
	
	public void TriggerOnEnter(GameObject sender)
	{
		if(!sender.TryGetComponent(out BulletEntity bulletEntity))
		{
			return;
		}
		
		DetectAdjacentTiles(bulletEntity.GetMovementDirection(), sender.transform.position);
		DestroyAllDetectedCollidersIfPossible();
	}

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
		detectedColliders = new Collider2D[OVERLAP_BUFFER_SIZE];
	}

	private void DetectAdjacentTiles(Vector2 movementDirection, Vector2 senderHitPoint)
	{
		var point = GetOverlapPoint(movementDirection, senderHitPoint);
		var size = GetOverlapSize(movementDirection);

		Physics2D.OverlapBoxNonAlloc(point, size, 0f, detectedColliders, overlapLayerMask);
	}

	private Vector2 GetOverlapPoint(Vector2 movementDirection, Vector2 senderHitPoint)
	{
		var overlapPoint = transform.position;
		
		if(movementDirection.IsVertical())
		{
			overlapPoint.x = senderHitPoint.x;
		}
		else if(movementDirection.IsHorizontal())
		{
			overlapPoint.y = senderHitPoint.y;
		}

		return overlapPoint;
	}

	private Vector2 GetOverlapSize(Vector2 movementDirection)
	{
		var boundsPadding = Vector2.one*OVERLAP_BOUNDS_PADDING;
		var overlapSize = (Vector2)c2D.bounds.size;
		
		if(movementDirection.IsVertical())
		{
			overlapSize.x = OVERLAP_RANGE;
		}
		else if(movementDirection.IsHorizontal())
		{
			overlapSize.y = OVERLAP_RANGE;
		}

		return overlapSize - boundsPadding;
	}

	private void DestroyAllDetectedCollidersIfPossible()
	{
		foreach (var collider in detectedColliders)
		{
			if(collider != null)
			{
				Destroy(collider.gameObject);
			}
		}
	}
}