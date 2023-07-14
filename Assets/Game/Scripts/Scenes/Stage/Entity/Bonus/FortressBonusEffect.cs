using UnityEngine;

public class FortressBonusEffect : TimedBonusEffect
{
	[Min(0f)] public float overlapBoxSize = 1.5f;
	public LayerMask overlapLayers;
	public GameObject metalTile;
	
	public override void PerformEffect()
	{
		GameObject nuke = GameObject.FindGameObjectWithTag("Nuke");
		Collider2D nukeCollider = nuke.GetComponent<Collider2D>();
		Collider2D[] colliders = Physics2D.OverlapBoxAll(nuke.transform.position, Vector2.one*overlapBoxSize, 0f, overlapLayers);

		foreach (Collider2D collider in colliders)
		{
			Destroy(collider.gameObject);
		}

		for (int y = 0; y <= 2; ++y)
		{
			for (int x = 0; x <= 3; ++x)
			{
				Vector2 topLeftPosition = new Vector2(-1.25f, -6.25f);
				Vector2 offset = new Vector2(x, y)*0.5f;
				Vector2 finalPosition = topLeftPosition + offset;
				
				if(!nukeCollider.OverlapPoint(finalPosition))
				{
					Instantiate(metalTile, finalPosition, Quaternion.identity);
				}
			}
		}
	}
}