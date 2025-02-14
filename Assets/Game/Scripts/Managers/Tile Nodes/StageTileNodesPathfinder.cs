using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNodesPathfinder : MonoBehaviour
{
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color pathNodeGizmosColor = new(0.5f, 0f, 0.25f, 1f);
	[SerializeField, Range(0.01f, 0.25f)] private float pathNodeGizmosRadius = 0.05f;
	
	private StageTileNodesManager stageTileNodesManager;
	private TileNode startTileNode;
	private TileNode endTileNode;
	private bool foundPath;

	private readonly PriorityQueue<TileNode> priorityQueue = new();
	private readonly List<TileNode> pathNodes = new();
	
	public bool PathExistsBetweenTwoTileNodes(TileNode startTileNode, TileNode endTileNode)
	{
		this.startTileNode = startTileNode;
		this.endTileNode = endTileNode;

		ClearData();
		ResetTileNodes();
		InitiatePathfinder();
		FindPathToEndTileNode();
		
		return foundPath;
	}

	private void Awake()
	{
		stageTileNodesManager = ObjectMethods.FindComponentOfType<StageTileNodesManager>();
	}

	private void ClearData()
	{
		pathNodes.Clear();
		priorityQueue.Clear();
	}

	private void ResetTileNodes()
	{
		if(stageTileNodesManager != null)
		{
			stageTileNodesManager.ResetTileNodes();
		}
	}

	private void InitiatePathfinder()
	{
		if(startTileNode == null || endTileNode == null)
		{
			return;
		}
		
		foundPath = false;
		startTileNode.Weight = 0;

		AddTileNodeToQueue(startTileNode);
	}

	private void FindPathToEndTileNode()
	{
		while (!foundPath && priorityQueue.Count > 0)
		{
			VisitTileNodeIfNeeded(priorityQueue.Dequeue());
		}
	}

	private void VisitTileNodeIfNeeded(TileNode tileNode)
	{
		if(tileNode == null || tileNode.Visited || endTileNode == null)
		{
			return;
		}

		tileNode.Visited = true;

		OperateOnTileNode(tileNode);
	}

	private void OperateOnTileNode(TileNode tileNode)
	{
		if(tileNode == null)
		{
			return;
		}
		
		if(tileNode == endTileNode)
		{
			foundPath = true;

			PathfindingMethods.OperateOnPathTileNodes(tileNode, tileNode => pathNodes.Add(tileNode));
		}
		else
		{
			AddNeighborsOf(tileNode);
		}
	}

	private void AddNeighborsOf(TileNode tileNode)
	{
		if(tileNode == null)
		{
			return;
		}
		
		var neighbors = tileNode.GetNeighbors().Where(neighbor => neighbor.Passable && !neighbor.Visited).ToList();

		neighbors?.ForEach(neighbor => SetupAndAddNeighbor(neighbor, tileNode));
	}

	private void SetupAndAddNeighbor(TileNode neighboringTileNode, TileNode parentTileNode)
	{
		neighboringTileNode.Parent = parentTileNode;

		AddTileNodeToQueue(neighboringTileNode);
	}

	private void AddTileNodeToQueue(TileNode tileNode)
	{
		if(tileNode == null)
		{
			return;
		}
		
		var tileNodeCost = tileNode.GetCostToReachTo(endTileNode);
		var tileNodeWithCost = new PriorityQueueElement<TileNode>(tileNode, tileNodeCost);
		
		priorityQueue.Enqueue(tileNodeWithCost);
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos && pathNodes.Count > 0)
		{
			GizmosMethods.OperateOnGizmos(() => pathNodes.ForEach(pathNode => Gizmos.DrawSphere(pathNode.GetPosition(), pathNodeGizmosRadius)), pathNodeGizmosColor);
		}
	}
}