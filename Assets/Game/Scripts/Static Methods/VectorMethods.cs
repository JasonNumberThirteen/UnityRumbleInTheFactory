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

	public static List<Vector2> GetCardinalDirections(float offset = 0)
	{
		var directions = new List<Vector2>()
		{
			Vector2.up*offset,
			Vector2.down*offset,
			Vector2.left*offset,
			Vector2.right*offset
		};

		return directions;
	}
}