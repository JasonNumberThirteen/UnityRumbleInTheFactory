using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNode : MonoBehaviour
{
	public bool Passable {get; set;}
	public bool Visited {get; set;}
	public int Weight {get; set;}
	public StageTileNode Parent {get; set;}

	private StageLayoutManager stageLayoutManager;
	
	private readonly List<StageTileNode> neighbors = new();

	public Vector2 GetPosition() => transform.position;
	public List<StageTileNode> GetNeighbors() => neighbors;
	public float GetCostToReachTo(StageTileNode endStageTileNode) => GetPathLengthFromStart() + GetDistanceTo(endStageTileNode);

	public void ResetData()
	{
		Visited = false;
		Weight = 0;
		Parent = null;
	}

	public void FindNeighbors(List<StageTileNode> stageTileNodes)
	{
		neighbors.Clear();
		
		if(stageLayoutManager == null)
		{
			return;
		}
		
		var tileSize = stageLayoutManager.GetTileSize();
		var allDirections = new List<Vector2>()
		{
			Vector2.up*tileSize,
			Vector2.down*tileSize,
			Vector2.left*tileSize,
			Vector2.right*tileSize
		};
		
		allDirections.ForEach(vector => AddNeighborIfPossible(stageTileNodes, vector));
	}

	private void Awake()
	{
		stageLayoutManager = ObjectMethods.FindComponentOfType<StageLayoutManager>();
	}

	private void AddNeighborIfPossible(List<StageTileNode> stageTileNodes, Vector2 direction)
	{
		var neighboringPosition = GetPosition() + direction;
		var neighboringStageTileNode = stageTileNodes.FirstOrDefault(stageTileNode => stageTileNode.GetPosition() == neighboringPosition);

		if(neighboringStageTileNode != null)
		{
			neighbors.Add(neighboringStageTileNode);
		}
	}

	private int GetPathLengthFromStart()
	{
		var length = 0;

		PathfindingMethods.OperateOnPathStageTileNodes(this, stageTileNode => length += stageTileNode.Weight);

		return length;
	}

	private float GetDistanceTo(StageTileNode stageTileNode) => stageTileNode != null ? PathfindingMethods.GetManhattanDistance(GetPosition(), stageTileNode.GetPosition()) : float.MaxValue;
}