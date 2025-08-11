using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNode : MonoBehaviour
{
	public bool Passable {get; set;}
	public bool Visited {get; set;}
	public int Weight {get; set;}
	
	private StageLayoutManager stageLayoutManager;
	
	private readonly StageTileNodeData stageTileNodeData = new();
	private readonly List<StageTileNode> neighbours = new();

	public Vector2 GetPosition() => transform.position;
	public List<StageTileNode> GetNeighbours() => neighbours;
	public StageTileNodeData GetStageTileNodeData() => stageTileNodeData;
	//public float GetCostToReachTo(StageTileNode endStageTileNode) => GetPathLengthFromStart() + GetDistanceTo(endStageTileNode);

	public void ResetData()
	{
		Visited = false;

		stageTileNodeData.SetValues(null, 0, 0);
		// Weight = 0;
		// Parent = null;
	}

	public void FindNeighbours(List<StageTileNode> stageTileNodes)
	{
		neighbours.Clear();
		
		if(stageLayoutManager == null)
		{
			return;
		}
		
		var directions = VectorMethods.GetCardinalDirections(stageLayoutManager.GetTileSize());
		
		directions.ForEach(vector => AddNeighbourIfPossible(stageTileNodes, vector));
	}

	private void Awake()
	{
		stageLayoutManager = ObjectMethods.FindComponentOfType<StageLayoutManager>();
	}

	private void AddNeighbourIfPossible(List<StageTileNode> stageTileNodes, Vector2 direction)
	{
		var neighbouringPosition = GetPosition() + direction;
		var neighbouringStageTileNode = stageTileNodes.FirstOrDefault(stageTileNode => stageTileNode.GetPosition() == neighbouringPosition);

		if(neighbouringStageTileNode != null)
		{
			neighbours.Add(neighbouringStageTileNode);
		}
	}

	// private int GetPathLengthFromStart()
	// {
	// 	var length = 0;

	// 	PathfindingMethods.OperateOnPathStageTileNodes(this, stageTileNode => length += stageTileNode.Weight);

	// 	return length;
	// }

	// private float GetDistanceTo(StageTileNode stageTileNode) => stageTileNode != null ? PathfindingMethods.GetManhattanDistance(GetPosition(), stageTileNode.GetPosition()) : float.MaxValue;
}