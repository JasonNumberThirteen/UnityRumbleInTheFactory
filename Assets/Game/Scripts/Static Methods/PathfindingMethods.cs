using System;
using UnityEngine;

public static class PathfindingMethods
{
	public static void OperateOnPathTileNodes(TileNode tileNodeToStartFrom, Action<TileNode> action)
	{
		var currentTileNode = tileNodeToStartFrom;

		while (currentTileNode.Parent != null)
		{
			action?.Invoke(currentTileNode);

			currentTileNode = currentTileNode.Parent;
		}
	}
	
	public static float GetManhattanDistance(Vector2 positionA, Vector2 positionB)
	{
		var distanceX = GetDistanceInOneDimension(positionA.x, positionB.x);
		var distanceY = GetDistanceInOneDimension(positionA.y, positionB.y);

		return distanceX + distanceY;
	}
	
	private static float GetDistanceInOneDimension(float a, float b) => Mathf.Abs(a - b);
}