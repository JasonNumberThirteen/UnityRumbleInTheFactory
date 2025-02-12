using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TileNode : MonoBehaviour
{
	public bool Passable {get; set;}
	public bool Visited {get; set;}
	public int Weight {get; set;}
	public TileNode Parent {get; set;}

	private StageLayoutManager stageLayoutManager;
	
	private readonly List<TileNode> neighbors = new();

	public Vector2 GetPosition() => transform.position;
	public List<TileNode> GetNeighbors() => neighbors;
	public float GetCostToReachTo(TileNode endTileNode) => GetPathLengthFromStart() + GetDistanceTo(endTileNode);

	public void ResetData()
	{
		Visited = false;
		Weight = 0;
		Parent = null;
	}

	public void FindNeighbors(List<TileNode> tileNodes)
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
		
		allDirections.ForEach(vector => AddNeighborIfPossible(tileNodes, vector));
	}

	private void Awake()
	{
		stageLayoutManager = ObjectMethods.FindComponentOfType<StageLayoutManager>();
	}

	private void AddNeighborIfPossible(List<TileNode> tileNodes, Vector2 direction)
	{
		var neighboringPosition = GetPosition() + direction;
		var neighboringNode = tileNodes.FirstOrDefault(tileNode => tileNode.GetPosition() == neighboringPosition);

		if(neighboringNode != null)
		{
			neighbors.Add(neighboringNode);
		}
	}

	private int GetPathLengthFromStart()
	{
		var length = 0;

		PathfindingMethods.OperateOnPathTileNodes(this, tileNode => length += tileNode.Weight);

		return length;
	}

	private float GetDistanceTo(TileNode tileNode) => tileNode != null ? PathfindingMethods.GetManhattanDistance(GetPosition(), tileNode.GetPosition()) : float.MaxValue;
}