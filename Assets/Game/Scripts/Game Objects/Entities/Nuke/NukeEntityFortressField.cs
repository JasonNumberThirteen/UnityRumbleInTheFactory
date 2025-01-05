using UnityEngine;

[RequireComponent(typeof(Nuke))]
public class NukeEntityFortressField : MonoBehaviour
{
	[SerializeField] private Rect area;
	[SerializeField] private LayerMask overlapLayerMask;
	[SerializeField] private GameObject tilePrefab;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color areaGizmosColor = Color.yellow;

	private Nuke nuke;
	private float fortressDuration;

	public void BuildFortress(float duration)
	{
		fortressDuration = duration;
		
		DestroyAllGOsWithinArea();
		InstantiateTilesWithinArea();
	}

	private void Awake()
	{
		nuke = GetComponent<Nuke>();
	}
	
	private void DestroyAllGOsWithinArea()
	{
		var colliders = Physics2D.OverlapBoxAll(area.position, area.size, 0f, overlapLayerMask);

		foreach (var collider in colliders)
		{
			Destroy(collider.gameObject);
		}
	}

	private void InstantiateTilesWithinArea()
	{
		for (var y = area.yMin; y < area.yMax; y += 0.5f)
		{
			for (var x = area.xMin; x < area.xMax; x += 0.5f)
			{
				InstantiateTile(GetTilePosition(x, y));
			}
		}
	}

	private Vector2 GetTilePosition(float leftSideX, float topSideY)
	{
		var x = leftSideX - area.width*0.5f + 0.25f;
		var y = topSideY - area.height*0.5f + 0.25f;
		
		return new Vector2(x, y);
	}

	private void InstantiateTile(Vector2 position)
	{
		if(tilePrefab == null || nuke.OverlapPoint(position))
		{
			return;
		}

		var instance = Instantiate(tilePrefab, position, Quaternion.identity);
		
		if(instance.TryGetComponent(out Timer timer))
		{
			timer.duration = fortressDuration;
		}
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos)
		{
			GizmosMethods.OperateOnGizmos(() => Gizmos.DrawWireCube(area.position, area.size), areaGizmosColor);
		}
	}
}