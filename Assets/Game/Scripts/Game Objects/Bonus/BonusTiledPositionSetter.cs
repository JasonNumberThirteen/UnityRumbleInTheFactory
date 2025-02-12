using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BonusTiledPositionSetter : MonoBehaviour
{
	[SerializeField] private Rect area;
	[SerializeField, Min(0.01f)] private float gridSize = 0.5f;
	[SerializeField] private LayerMask unacceptableObjectsForColliderBoundsLayerMask;
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
		var randomPosition = GetRandomPosition();
		var gridPosition = GetGridPosition(randomPosition);
		
		if(PositionIsInaccessible(gridPosition))
		{
			return GetFinalPosition();
		}

		return gridPosition;
	}

	private Vector2 GetRandomPosition()
	{
		var x = Random.Range(area.xMin, area.xMax);
		var y = Random.Range(area.yMin, area.yMax);

		return new Vector2(x, y);
	}

	private Vector2 GetGridPosition(Vector2 position)
	{
		var x = GetGridCoordinate(position.x);
		var y = GetGridCoordinate(position.y);

		return new Vector2(x, y);
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
		var colliders = Physics2D.OverlapBoxAll(position, c2D.bounds.size, 0f, unacceptableObjectsForColliderBoundsLayerMask);

		return colliders.Any(collider => collider.gameObject != gameObject);
	}

	private bool DetectedPathToClosestPlayerRobotFrom(Vector2 position)
	{
		if(stageTileNodesManager == null || stageTileNodesPathfinder == null)
		{
			return false;
		}
		
		var startNodesAreaSize = Vector2.one;
		var startNodesArea = new Rect(position - startNodesAreaSize*0.5f, startNodesAreaSize);
		var availableStartNodes = stageTileNodesManager.GetTileNodesWithin(startNodesArea).Where(tileNode => tileNode.Passable);

		return availableStartNodes != null && availableStartNodes.Any(startNode => startNode != null && stageTileNodesPathfinder.PathExistsBetweenTwoTileNodes(startNode, stageTileNodesManager.GetTileNodeWhereClosestPlayerRobotIsOnIfPossible(startNode)));
	}

	private float GetGridCoordinate(float coordinate) => Mathf.Round(coordinate / gridSize)*gridSize;
	private bool PositionIsInaccessible(Vector2 position) => DetectedAnyUnacceptableCollider(position) || !DetectedPathToClosestPlayerRobotFrom(position);
}