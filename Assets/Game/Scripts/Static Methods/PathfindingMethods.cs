using System;
using UnityEngine;

public static class PathfindingMethods
{
	public static void OperateOnPathStageTileNodes(StageTileNode stageTileNodeToStartFrom, Action<StageTileNode> action)
	{
		var currentStageTileNode = stageTileNodeToStartFrom;

		while (currentStageTileNode.Parent != null)
		{
			action?.Invoke(currentStageTileNode);

			currentStageTileNode = currentStageTileNode.Parent;
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