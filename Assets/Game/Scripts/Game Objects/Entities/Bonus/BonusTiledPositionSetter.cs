using Random = UnityEngine.Random;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BonusTiledPositionSetter : MonoBehaviour
{
	[SerializeField] private Rect area;
	[SerializeField, Min(0.01f)] private float gridSize = 0.5f;
	[SerializeField] private LayerMask inaccessibleAreaLayers;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color accessiblePositionGizmosColor = new(0f, 1f, 0f, 0.5f);
	[SerializeField] private Color inaccessiblePositionGizmosColor = new(1f, 0f, 0f, 0.5f);

	private Collider2D c2D;

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
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

	private bool PositionIsInaccessible(Vector2 position)
	{
		var colliders = GetColliders(position);

		return colliders.Length >= 4 || colliders.Any(ColliderFitsEntireBounds);
	}

	private Collider2D[] GetColliders(Vector2 position)
	{
		var colliders = Physics2D.OverlapBoxAll(position, c2D.bounds.size, 0f, inaccessibleAreaLayers);

		return colliders.Where(collider => collider.OverlapPoint(position)).ToArray();
	}

	private float GetGridCoordinate(float coordinate) => Mathf.Round(coordinate / gridSize)*gridSize;
	private bool ColliderFitsEntireBounds(Collider2D collider2D) => collider2D.bounds.Contains(c2D.bounds.min) && collider2D.bounds.Contains(c2D.bounds.max);
}