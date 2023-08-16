using Random = UnityEngine.Random;
using UnityEngine;

public class BonusPlacement : MonoBehaviour
{
	public Vector2 bottomLeftArea, topRightArea;
	[Min(0.01f)] public float gridSize = 0.5f;
	[Min(0.01f)] public float overlapCircleRadius = 0.5f;
	public LayerMask excludedLayers;

	private void Start() => transform.position = BonusPosition();
	private float RandomCoordinate(float min, float max) => Random.Range(min, max);
	private float GridCoordinate(float coordinate) => Mathf.Round(coordinate / gridSize)*gridSize;

	private Vector2 BonusPosition()
	{
		Vector2 randomPosition = RandomPosition();
		Vector2 gridPosition = GridPosition(randomPosition);
		
		if(PositionIsInaccessible(gridPosition))
		{
			return BonusPosition();
		}

		return gridPosition;
	}

	private Vector2 RandomPosition()
	{
		float x = RandomCoordinate(bottomLeftArea.x, topRightArea.x);
		float y = RandomCoordinate(bottomLeftArea.y, topRightArea.y);

		return new Vector2(x, y);
	}

	private Vector2 GridPosition(Vector2 position)
	{
		float x = GridCoordinate(position.x);
		float y = GridCoordinate(position.y);

		return new Vector2(x, y);
	}

	private bool PositionIsInaccessible(Vector2 position)
	{
		if(TryGetComponent(out Collider2D collider2D))
		{
			Collider2D[] colliders = Physics2D.OverlapBoxAll(position, collider2D.bounds.size, 0f, excludedLayers);
			int excludedPositions = 0;

			foreach (Collider2D collider in colliders)
			{
				if(collider.OverlapPoint(position))
				{
					++excludedPositions;
				}
			}

			return colliders.Length > 0 && excludedPositions == 4;
		}

		return false;
	}
	
	private void OnDrawGizmos()
	{
		if(TryGetComponent(out Collider2D collider2D))
		{
			Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, collider2D.bounds.size, 0f, excludedLayers);
		
			foreach (Collider2D collider in colliders)
			{
				Gizmos.color = collider.OverlapPoint(transform.position) ? Color.red : Color.green;
				
				Gizmos.DrawWireCube(collider.transform.position, collider.bounds.size);
			}
		}
	}
}