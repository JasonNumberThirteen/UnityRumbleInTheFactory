using UnityEngine;

[RequireComponent(typeof(Nuke))]
public class NukeFortressField : MonoBehaviour
{
	[SerializeField, Min(0f)] private float overlapBoxSize = 1.5f;
	[SerializeField] private LayerMask overlapLayerMask;
	[SerializeField] private GameObject metalTilePrefab;

	private Nuke nuke;
	private float fortressDuration;

	public void BuildFortress(float duration)
	{
		fortressDuration = duration;
		
		DestroyAllGOsAroundNuke();
		InstantiateMetalTiles();
	}

	private void Awake()
	{
		nuke = GetComponent<Nuke>();
	}
	
	private void DestroyAllGOsAroundNuke()
	{
		var colliders = Physics2D.OverlapBoxAll(nuke.transform.position, Vector2.one*overlapBoxSize, 0f, overlapLayerMask);

		foreach (var collider in colliders)
		{
			Destroy(collider.gameObject);
		}
	}

	private void InstantiateMetalTiles()
	{
		for (int y = 0; y <= 2; ++y)
		{
			for (int x = 0; x <= 3; ++x)
			{
				InstantiateMetalTile(x, y);
			}
		}
	}

	private void InstantiateMetalTile(int x, int y)
	{
		var position = MetalTilePosition(x, y);

		if(metalTilePrefab == null || !MetalTileCanBePlaced(position))
		{
			return;
		}

		var instance = Instantiate(metalTilePrefab, position, Quaternion.identity);
			
		if(instance.TryGetComponent(out Timer timer))
		{
			timer.duration = fortressDuration;
		}
	}

	private Vector2 MetalTilePosition(int x, int y)
	{
		var topLeftPosition = new Vector2(-1.25f, -6.25f);
		var offset = new Vector2(x, y)*0.5f;
		
		return topLeftPosition + offset;
	}

	private bool MetalTileCanBePlaced(Vector2 position) => nuke != null && nuke.TryGetComponent(out Collider2D collider2D) && !collider2D.OverlapPoint(position);
}