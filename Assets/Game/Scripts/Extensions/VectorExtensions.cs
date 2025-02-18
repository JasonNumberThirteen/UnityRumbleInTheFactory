using UnityEngine;

public static class VectorExtensions
{
	public static bool IsHorizontal(this Vector2 vector) => vector == Vector2.left || vector == Vector2.right;
	public static bool IsVertical(this Vector2 vector) => vector == Vector2.up || vector == Vector2.down;
	public static bool IsZero(this Vector2 vector) => vector == Vector2.zero;
	public static Vector2 GetOffsetFrom(this Vector2 vector, Vector2 position) => vector - position*0.5f;

	public static Vector2 GetRawVector(this Vector2 vector)
	{
		if(vector.IsZero())
		{
			return Vector2.zero;
		}

		return Mathf.Abs(vector.x) > Mathf.Abs(vector.y) ? Vector2.right*Mathf.Sign(vector.x) : Vector2.up*Mathf.Sign(vector.y);
	}

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

	public static Vector2 ToTiledPosition(this Vector2 vector, float tileSize)
	{
		var x = MathMethods.GetTiledCoordinate(vector.x, tileSize);
		var y = MathMethods.GetTiledCoordinate(vector.y, tileSize);

		return new Vector2(x, y);
	}
}