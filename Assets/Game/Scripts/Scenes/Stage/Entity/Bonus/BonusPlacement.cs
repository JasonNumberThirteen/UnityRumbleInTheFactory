using Random = UnityEngine.Random;
using UnityEngine;

public class BonusPlacement : MonoBehaviour
{
	public Vector2 bottomLeftArea, topRightArea;
	[Min(0.01f)] public float gridSize = 0.5f;
	[Min(0.01f)] public float overlapCircleRadius = 0.5f;
	public LayerMask excludedLayers;

	private void Start() => transform.position = RandomPosition();

	private Vector2 RandomPosition()
	{
		float x = Random.Range(bottomLeftArea.x, topRightArea.x);
		float y = Random.Range(bottomLeftArea.y, topRightArea.y);
		float gridX = Mathf.Round(x / gridSize)*gridSize;
		float gridY = Mathf.Round(y / gridSize)*gridSize;
		Vector2 position = new Vector2(gridX, gridY);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(position, overlapCircleRadius, excludedLayers);
		int excludedPositions = 0;

		foreach (Collider2D collider in colliders)
		{
			if(collider.OverlapPoint(position))
			{
				++excludedPositions;

				Debug.Log("Excluded positions: " + excludedPositions);
			}
		}

		if(colliders.Length > 0 && excludedPositions == colliders.Length)
		{
			Debug.Log("Recursion at: (" + position.x + "; " + position.y + ")");

			return RandomPosition();
		}

		return position;
	}

	private void OnDrawGizmos()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapCircleRadius, excludedLayers);
		int excludedPositions = 0;
		
		foreach (Collider2D collider in colliders)
		{
			Gizmos.color = Color.green;

			if(collider.OverlapPoint(transform.position))
			{
				Gizmos.color = Color.red;
				++excludedPositions;
			}
			
			Gizmos.DrawWireCube(collider.transform.position, collider.bounds.size);
		}
	}
}