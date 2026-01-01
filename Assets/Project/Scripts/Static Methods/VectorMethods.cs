using System.Collections.Generic;
using UnityEngine;

public static class VectorMethods
{
	public static Vector2 GetRandomPositionWithin(Rect area)
	{
		var x = Random.Range(area.xMin, area.xMax);
		var y = Random.Range(area.yMin, area.yMax);

		return new Vector2(x, y);
	}

	public static List<Vector2> GetCardinalDirections(float length = 1f)
	{
		var directions = new List<Vector2>()
		{
			Vector2.up*length,
			Vector2.down*length,
			Vector2.left*length,
			Vector2.right*length
		};

		return directions;
	}
}