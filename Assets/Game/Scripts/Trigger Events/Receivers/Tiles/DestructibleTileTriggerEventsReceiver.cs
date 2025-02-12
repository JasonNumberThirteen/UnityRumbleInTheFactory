using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DestructibleTileTriggerEventsReceiver : MonoBehaviour, ITriggerableOnEnter
{
	[SerializeField] private LayerMask overlapLayerMask;

	protected StageSoundManager stageSoundManager;
	
	private Collider2D c2D;
	private Collider2D[] detectedColliders;
	private StageTileNodesManager stageTileNodesManager;

	private readonly int OVERLAP_BUFFER_SIZE = 4;
	private readonly float OVERLAP_RANGE = 0.75f;
	private readonly float OVERLAP_BOUNDS_PADDING = 0.05f;
	
	public virtual void TriggerOnEnter(GameObject sender)
	{
		if(!sender.TryGetComponent(out BulletEntity bulletEntity))
		{
			return;
		}
		
		DetectAdjacentTiles(bulletEntity.GetMovementDirection(), sender.transform.position);
		DestroyAllDetectedCollidersIfPossible(sender.TryGetComponent(out PlayerRobotEntityBulletEntity _));
	}

	protected virtual void Awake()
	{
		c2D = GetComponent<Collider2D>();
		detectedColliders = new Collider2D[OVERLAP_BUFFER_SIZE];
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		stageTileNodesManager = ObjectMethods.FindComponentOfType<StageTileNodesManager>();
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

	private void DestroyAllDetectedCollidersIfPossible(bool playSound)
	{
		var destroyedAtLeastOneCollider = false;
		
		detectedColliders.ForEach(collider =>
		{
			if(collider != null)
			{
				Destroy(collider.gameObject);
				SetNodeAsPassableIfNeeded(collider.transform.position);

				destroyedAtLeastOneCollider = true;
			}
		});

		PlaySoundIfNeeded(destroyedAtLeastOneCollider && playSound);
	}

	private void SetNodeAsPassableIfNeeded(Vector2 position)
	{
		if(stageTileNodesManager == null)
		{
			return;
		}

		var node = stageTileNodesManager.GetClosestTileNodeTo(position);

		if(node != null && !node.Passable)
		{
			node.Passable = true;
		}
	}

	private void PlaySoundIfNeeded(bool playSound)
	{
		if(playSound && stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.PlayerRobotDestructibleTileDestruction);
		}
	}
}