using UnityEngine;

public class FortressBonusTrigger : TimedBonusTrigger
{
	public string nukeTag;
	[Min(0f)] public float overlapBoxSize = 1.5f;
	public LayerMask overlapLayers;
	public GameObject metalTile;

	private GameObject nuke;
	
	public override void TriggerOnEnter(GameObject sender)
	{
		nuke = GameObject.FindGameObjectWithTag(nukeTag);

		if(nuke != null)
		{
			DestroyAllObjectsAroundTheNuke();
			InstantiateMetalTilesAroundTheNuke();
		}

		base.TriggerOnEnter(sender);
	}
	
	private void DestroyAllObjectsAroundTheNuke()
	{
		Vector2 size = Vector2.one*overlapBoxSize;
		Collider2D[] colliders = Physics2D.OverlapBoxAll(nuke.transform.position, size, 0f, overlapLayers);

		foreach (Collider2D collider in colliders)
		{
			Destroy(collider.gameObject);
		}
	}

	private void InstantiateMetalTilesAroundTheNuke()
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
		Vector2 position = MetalTilePosition(x, y);

		if(MetalTileCanBePlaced(position))
		{
			GameObject instance = Instantiate(metalTile, position, Quaternion.identity);
			
			SetMetalTileTimer(instance);
		}
	}

	private Vector2 MetalTilePosition(int x, int y)
	{
		Vector2 topLeftPosition = new Vector2(-1.25f, -6.25f);
		Vector2 offset = new Vector2(x, y)*0.5f;
		
		return topLeftPosition + offset;
	}

	private bool MetalTileCanBePlaced(Vector2 position) => nuke.TryGetComponent(out Collider2D nukeCollider) && !nukeCollider.OverlapPoint(position);

	private void SetMetalTileTimer(GameObject metalTile)
	{
		if(metalTile.TryGetComponent(out Timer timer))
		{
			timer.duration = GetDuration();
		}
	}
}