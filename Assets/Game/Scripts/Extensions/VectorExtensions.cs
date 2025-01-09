using UnityEngine;

public static class VectorExtensions
{
	public static bool IsHorizontal(this Vector2 vector) => vector == Vector2.left || vector == Vector2.right;
	public static bool IsVertical(this Vector2 vector) => vector == Vector2.up || vector == Vector2.down;
}