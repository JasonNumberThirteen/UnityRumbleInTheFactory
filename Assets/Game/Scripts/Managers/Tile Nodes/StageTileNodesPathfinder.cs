using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNodesPathfinder : MonoBehaviour
{
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color pathNodeGizmosColor = new(0.5f, 0f, 0.25f, 1f);
	[SerializeField, Range(0.01f, 0.25f)] private float pathNodeGizmosRadius = 0.05f;
	
	private StageTileNode startStageTileNode;
	private StageTileNode endStageTileNode;
	private bool foundPath;
	private StageTileNodesManager stageTileNodesManager;

	private readonly PriorityQueue<StageTileNode> priorityQueue = new();
	private readonly List<StageTileNode> pathStageTileNodes = new();
	
	public bool PathExistsBetweenTwoStageTileNodes(StageTileNode startStageTileNode, StageTileNode endStageTileNode)
	{
		this.startStageTileNode = startStageTileNode;
		this.endStageTileNode = endStageTileNode;

		ClearData();
		ResetStageTileNodes();
		InitiatePathfinder();
		FindPathToEndStageTileNode();
		
		return foundPath;
	}

	private void Awake()
	{
		stageTileNodesManager = ObjectMethods.FindComponentOfType<StageTileNodesManager>();
	}

	private void ClearData()
	{
		pathStageTileNodes.Clear();
		priorityQueue.Clear();
	}

	private void ResetStageTileNodes()
	{
		if(stageTileNodesManager != null)
		{
			stageTileNodesManager.ResetStageTileNodes();
		}
	}

	private void InitiatePathfinder()
	{
		if(startStageTileNode == null || endStageTileNode == null)
		{
			return;
		}
		
		foundPath = false;
		startStageTileNode.Weight = 0;

		AddStageTileNodeToQueue(startStageTileNode);
	}

	private void FindPathToEndStageTileNode()
	{
		while (!foundPath && priorityQueue.Count > 0)
		{
			VisitStageTileNodeIfNeeded(priorityQueue.Dequeue());
		}
	}

	private void VisitStageTileNodeIfNeeded(StageTileNode stageTileNode)
	{
		if(stageTileNode == null || stageTileNode.Visited || endStageTileNode == null)
		{
			return;
		}

		stageTileNode.Visited = true;

		OperateOnStageTileNode(stageTileNode);
	}

	private void OperateOnStageTileNode(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return;
		}
		
		if(stageTileNode == endStageTileNode)
		{
			FinishSearchingOn(stageTileNode);
		}
		else
		{
			AddNeighborsOf(stageTileNode);
		}
	}

	private void FinishSearchingOn(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return;
		}
		
		foundPath = true;

		PathfindingMethods.OperateOnPathStageTileNodes(stageTileNode, stageTileNode => pathStageTileNodes.Add(stageTileNode));
	}

	private void AddNeighborsOf(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return;
		}
		
		var neighbors = stageTileNode.GetNeighbors().Where(neighbor => neighbor.Passable && !neighbor.Visited).ToList();

		neighbors?.ForEach(neighbor => SetupAndAddNeighbor(neighbor, stageTileNode));
	}

	private void SetupAndAddNeighbor(StageTileNode neighboringStageTileNode, StageTileNode parentStageTileNode)
	{
		neighboringStageTileNode.Parent = parentStageTileNode;

		AddStageTileNodeToQueue(neighboringStageTileNode);
	}

	private void AddStageTileNodeToQueue(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return;
		}
		
		var stageTileNodeCost = stageTileNode.GetCostToReachTo(endStageTileNode);
		var stageTileNodeWithCost = new PriorityQueueElement<StageTileNode>(stageTileNode, stageTileNodeCost);
		
		priorityQueue.Enqueue(stageTileNodeWithCost);
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos && pathStageTileNodes.Count > 0)
		{
			GizmosMethods.OperateOnGizmos(() => pathStageTileNodes.ForEach(stageTileNode => Gizmos.DrawSphere(stageTileNode.GetPosition(), pathNodeGizmosRadius)), pathNodeGizmosColor);
		}
	}
}