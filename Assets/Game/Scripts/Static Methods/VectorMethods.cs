using UnityEngine;

public static class VectorMethods
{
	public static Vector2 GetRandomPositionWithin(Rect area)
	{
		var x = Random.Range(area.xMin, area.xMax);
		var y = Random.Range(area.yMin, area.yMax);

		return new Vector2(x, y);
	}
}