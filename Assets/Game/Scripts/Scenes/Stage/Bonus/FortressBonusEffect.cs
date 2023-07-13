using UnityEngine;

public class FortressBonusEffect : BonusEffect
{
	[Min(0f)] public float overlapBoxSize = 2.5f;
	public LayerMask overlapLayers;
	
	public override void PerformEffect()
	{
		GameObject nuke = GameObject.FindGameObjectWithTag("Nuke");
		Collider2D[] colliders = Physics2D.OverlapBoxAll(nuke.transform.position, Vector2.one*overlapBoxSize, 0f, overlapLayers);

		foreach (Collider2D collider in colliders)
		{
			Debug.Log(collider.gameObject.name);
			Destroy(collider.gameObject);
		}
	}
}