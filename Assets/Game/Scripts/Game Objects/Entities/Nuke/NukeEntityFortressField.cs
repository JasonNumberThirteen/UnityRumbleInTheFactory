using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NukeEntityFortressField : MonoBehaviour
{
	[SerializeField] private LayerMask overlapLayerMask;
	[SerializeField] private GameObject tilePrefab;
	[SerializeField, Min(0.01f)] private float gridSize = 0.5f;

	private NukeEntity nukeEntity;
	private Collider2D c2D;
	private float fortressDuration;

	public void BuildFortress(float duration)
	{
		fortressDuration = duration;
		
		DestroyAllGOsWithinArea();
		SpawnTilesWithinArea();
	}

	public void DestroyAllGOsWithinArea()
	{
		var colliders = Physics2D.OverlapBoxAll(c2D.bounds.center, c2D.bounds.size, 0f, overlapLayerMask);

		foreach (var collider in colliders)
		{
			Destroy(collider.gameObject);
		}
	}

	private void Awake()
	{
		nukeEntity = GetComponentInParent<NukeEntity>();
		c2D = GetComponent<Collider2D>();
	}

	private void SpawnTilesWithinArea()
	{
		for (var y = c2D.bounds.min.y; y < c2D.bounds.max.y; y += gridSize)
		{
			for (var x = c2D.bounds.min.x; x < c2D.bounds.max.x; x += gridSize)
			{
				SpawnTile(GetTilePosition(x, y));
			}
		}
	}

	private Vector2 GetTilePosition(float leftSideX, float topSideY)
	{
		var halfOfGridSize = gridSize*0.5f;
		var x = leftSideX + halfOfGridSize;
		var y = topSideY + halfOfGridSize;
		
		return new Vector2(x, y);
	}

	private void SpawnTile(Vector2 position)
	{
		if(tilePrefab == null || (nukeEntity != null && nukeEntity.OverlapPoint(position)))
		{
			return;
		}

		var instance = Instantiate(tilePrefab, position, Quaternion.identity);
		
		if(instance.TryGetComponent(out Timer timer))
		{
			timer.duration = fortressDuration;
		}
	}
}