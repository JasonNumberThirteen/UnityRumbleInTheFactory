using UnityEngine;

public class FortressBonusEffect : BonusEffect
{
	[Min(0f)] public float overlapBoxSize = 2.5f;
	public LayerMask overlapLayers;
	public GameObject metalTile;
	
	public override void PerformEffect()
	{
		GameObject nuke = GameObject.FindGameObjectWithTag("Nuke");
		Collider2D[] colliders = Physics2D.OverlapBoxAll(nuke.transform.position, Vector2.one*overlapBoxSize, 0f, overlapLayers);

		foreach (Collider2D collider in colliders)
		{
			Destroy(collider.gameObject);
		}

		for (int y = 0; y <= 3; ++y)
		{
			for (int x = 0; x <= 5; ++x)
			{
				if(x <= 1 || x >= 4 || y >= 2)
				{
					Vector2 topLeftPosition = new Vector2(-1.75f, -6.25f);
					Vector2 offset = new Vector2(x, y)*0.5f;
					
					Instantiate(metalTile, topLeftPosition + offset, Quaternion.identity);
				}
			}
		}
	}
}