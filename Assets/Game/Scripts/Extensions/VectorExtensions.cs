using UnityEngine;

public static class VectorExtensions
{
	public static bool IsHorizontal(this Vector2 vector) => vector == Vector2.left || vector == Vector2.right;
	public static bool IsVertical(this Vector2 vector) => vector == Vector2.up || vector == Vector2.down;
	public static bool IsZero(this Vector2 vector) => vector == Vector2.zero;

	public static bool OverlapsWithAnyOfColliders(this Vector2 vector, Collider2D[] colliders)
	{
		if(colliders == null)
		{
			return false;
		}
		
		foreach (var collider in colliders)
		{
			if(collider != null && collider.OverlapPoint(vector))
			{
				return true;
			}
		}

		return false;
	}
}