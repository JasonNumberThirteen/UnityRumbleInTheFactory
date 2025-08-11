using UnityEngine;

public static class DistanceMethods
{
	public static float GetManhattanDistance(Vector2 positionA, Vector2 positionB)
	{
		var distanceInXAxis = GetOneDimensionalDistance(positionA.x, positionB.x);
		var distanceInYAxis = GetOneDimensionalDistance(positionA.y, positionB.y);

		return distanceInXAxis + distanceInYAxis;
	}
	
	public static float GetOneDimensionalDistance(float a, float b) => Mathf.Abs(a - b);
}