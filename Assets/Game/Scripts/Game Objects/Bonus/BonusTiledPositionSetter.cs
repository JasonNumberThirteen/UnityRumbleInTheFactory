using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BonusTiledPositionSetter : MonoBehaviour
{
	[SerializeField] private Rect area;
	[SerializeField, Min(0.01f)] private float tileSize = 0.5f;
	[SerializeField] private LayerMask unacceptableGameObjects;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color accessiblePositionGizmosColor = new(0f, 1f, 0f, 0.5f);
	[SerializeField] private Color inaccessiblePositionGizmosColor = new(1f, 0f, 0f, 0.5f);

	private Collider2D c2D;
	private StageTileNodesManager stageTileNodesManager;
	private StageTileNodesPathfinder stageTileNodesPathfinder;

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
		stageTileNodesManager = ObjectMethods.FindComponentOfType<StageTileNodesManager>();
		stageTileNodesPathfinder = ObjectMethods.FindComponentOfType<StageTileNodesPathfinder>();
	}

	private void Start()
	{
		transform.position = GetFinalPosition();
	}

	private Vector2 GetFinalPosition()
	{
		var randomPosition = VectorMethods.GetRandomPositionWithin(area);
		var tiledPosition = randomPosition.ToTiledPosition(tileSize);
		
		return PositionIsInaccessible(tiledPosition) ? GetFinalPosition() : tiledPosition;
	}
	
	private void OnDrawGizmos()
	{
		if(!drawGizmos)
		{
			return;
		}
		
		if(c2D == null)
		{
			c2D = GetComponent<Collider2D>();
		}

		var color = PositionIsInaccessible(transform.position) ? inaccessiblePositionGizmosColor : accessiblePositionGizmosColor;
			
		GizmosMethods.OperateOnGizmos(() => Gizmos.DrawCube(transform.position, c2D.bounds.size), color);
	}

	private bool DetectedAnyUnacceptableCollider(Vector2 position)
	{
		var colliders = Physics2D.OverlapBoxAll(position, c2D.bounds.size, 0f, unacceptableGameObjects);

		return colliders.Any(collider => collider.gameObject != gameObject);
	}

	private bool DetectedPathToClosestPlayerRobotFrom(Vector2 position)
	{
		if(stageTileNodesManager == null || stageTileNodesPathfinder == null)
		{
			return false;
		}
		
		var startNodesAreaSize = Vector2.one;
		var startNodesArea = new Rect(position.GetOffsetFrom(startNodesAreaSize), startNodesAreaSize);
		var availableStartNodes = stageTileNodesManager.GetStageTileNodesWithin(startNodesArea).Where(tileNode => tileNode.Passable);

		return availableStartNodes != null && availableStartNodes.Any(startNode => startNode != null && stageTileNodesPathfinder.PathExistsBetweenTwoStageTileNodes(startNode, stageTileNodesManager.GetStageTileNodeWhereClosestPlayerRobotIsOnIfPossible(startNode)));
	}

	private bool PositionIsInaccessible(Vector2 position) => DetectedAnyUnacceptableCollider(position) || !DetectedPathToClosestPlayerRobotFrom(position);
}