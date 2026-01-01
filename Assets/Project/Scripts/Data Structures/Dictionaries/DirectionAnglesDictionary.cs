using UnityEngine;
using System.Collections.Generic;

public class DirectionAnglesDictionary : Dictionary<Vector2, float>
{
	public DirectionAnglesDictionary()
	{
		this[Vector2.up] = 0f;
		this[Vector2.down] = 180f;
		this[Vector2.left] = 270f;
		this[Vector2.right] = 90f;
	}

	public float GetAngleBy(Vector2 direction) => TryGetValue(direction, out var angle) ? angle : 0f;
}