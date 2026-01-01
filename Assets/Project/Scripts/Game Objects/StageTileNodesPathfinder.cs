using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNodesPathfinder : MonoBehaviour
{
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color pathNodeGizmosColor = new(0.5f, 0f, 0.25f, 1f);
	[SerializeField, Range(0.01f, 0.25f)] private float pathNodeGizmosRadius = 0.05f;
	
	private readonly List<StageTileNode> pathStageTileNodes = new();
	private readonly List<StageTileNode> stageTileNodesWaitingForVisit = new();

	private bool pathWasFound;
	private StageTileNode startStageTileNode;
	private StageTileNode destinationStageTileNode;
	private StageTileNodesManager stageTileNodesManager;

	public bool PathExistsBetweenStageTileNodes(StageTileNode startStageTileNode, StageTileNode destinationStageTileNode)
	{
		this.startStageTileNode = startStageTileNode;
		this.destinationStageTileNode = destinationStageTileNode;

		ClearData();
		InitiatePathfinder();
		
		return pathWasFound;
	}

	private void Awake()
	{
		stageTileNodesManager = ObjectMethods.FindComponentOfType<StageTileNodesManager>();
	}

	private void ClearData()
	{
		ResetStageTileNodes();
		ResetPathStageTileNodes();
		stageTileNodesWaitingForVisit.Clear();
	}

	private void ResetStageTileNodes()
	{
		if(stageTileNodesManager != null)
		{
			stageTileNodesManager.ResetStageTileNodes();
		}
	}

	private void ResetPathStageTileNodes()
	{
		pathStageTileNodes.ForEach(pathStageTileNode => pathStageTileNode.ResetData());
		pathStageTileNodes.Clear();
	}

	private void InitiatePathfinder()
	{
		if(startStageTileNode == null || destinationStageTileNode == null)
		{
			return;
		}

		pathWasFound = false;
		
		InitiateStartStageTileNode();
		FindPathToDestination();
	}

	private void InitiateStartStageTileNode()
	{
		if(startStageTileNode != null)
		{
			stageTileNodesWaitingForVisit.Add(startStageTileNode);
		}
	}

	private void FindPathToDestination()
	{
		while (!pathWasFound && stageTileNodesWaitingForVisit.Count > 0)
		{
			var currentStageTileNode = stageTileNodesWaitingForVisit.OrderBy(stageTileNode => stageTileNode.GetStageTileNodeData().TotalCost).FirstOrDefault();

			VisitStageTileNodeIfPossible(currentStageTileNode);
		}
	}

	private void VisitStageTileNodeIfPossible(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return;
		}

		stageTileNode.Visited = true;

		stageTileNodesWaitingForVisit.Remove(stageTileNode);
		OperateOnStageTileNode(stageTileNode);
	}

	private void OperateOnStageTileNode(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return;
		}
		
		if(stageTileNode == destinationStageTileNode)
		{
			FinishSearchingOn(stageTileNode);
		}
		else
		{
			OperateOnNeighboursOf(stageTileNode);
		}
	}

	private void FinishSearchingOn(StageTileNode stageTileNode)
	{
		pathWasFound = true;
		
		if(stageTileNode != null)
		{
			DefinePathStageTileNodes(stageTileNode);
		}
	}

	private void DefinePathStageTileNodes(StageTileNode stageTileNodeToStartFrom)
	{
		if(stageTileNodeToStartFrom == null)
		{
			return;
		}
		
		var currentStageTileNode = stageTileNodeToStartFrom;
		var currentStageTileNodeData = currentStageTileNode.GetStageTileNodeData();

		while (currentStageTileNodeData.Parent != null)
		{
			pathStageTileNodes.Add(currentStageTileNode);

			currentStageTileNode = currentStageTileNodeData.Parent;
			currentStageTileNodeData = currentStageTileNode.GetStageTileNodeData();
		}	
	}

	private void OperateOnNeighboursOf(StageTileNode parentStageTileNode)
	{
		if(parentStageTileNode == null)
		{
			return;
		}

		var neighbours = parentStageTileNode.GetNeighbours().Where(neighbour => neighbour.Passable && !neighbour.Visited).ToList();

		neighbours.ForEach(neighbour => OperateOnNeighbourIfNeeded(parentStageTileNode, neighbour));
	}

	private void OperateOnNeighbourIfNeeded(StageTileNode parentStageTileNode, StageTileNode neighbouringStageTileNode)
	{
		if(parentStageTileNode == null || neighbouringStageTileNode == null)
		{
			return;
		}
		
		var totalCostToReachNeighbourFromParent = GetTotalCostToReachNeighbourFromParent(parentStageTileNode, neighbouringStageTileNode);
		var neighbourIsAlreadyWaitingForVisit = stageTileNodesWaitingForVisit.Contains(neighbouringStageTileNode);
		var neighbourMapTileNodeData = neighbouringStageTileNode.GetStageTileNodeData();

		if(neighbourIsAlreadyWaitingForVisit && totalCostToReachNeighbourFromParent >= neighbourMapTileNodeData.RealValue)
		{
			return;
		}

		neighbourMapTileNodeData.SetValues(parentStageTileNode, totalCostToReachNeighbourFromParent, DistanceMethods.GetManhattanDistance(neighbouringStageTileNode.GetPosition(), destinationStageTileNode.GetPosition()));

		if(!neighbourIsAlreadyWaitingForVisit)
		{
			stageTileNodesWaitingForVisit.Add(neighbouringStageTileNode);
		}
	}

	private float GetTotalCostToReachNeighbourFromParent(StageTileNode parentStageTileNode, StageTileNode neighbouringStageTileNode)
	{
		var parentStageTileNodeRealValue = parentStageTileNode != null ? parentStageTileNode.GetStageTileNodeData().RealValue : 0f;
		var neighbouringStageTileNodeWeight = neighbouringStageTileNode != null ? neighbouringStageTileNode.Weight : 0;
		
		return parentStageTileNodeRealValue + neighbouringStageTileNodeWeight;
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos && pathStageTileNodes.Count > 0)
		{
			GizmosMethods.OperateOnGizmos(() => pathStageTileNodes.ForEach(stageTileNode => Gizmos.DrawSphere(stageTileNode.GetPosition(), pathNodeGizmosRadius)), pathNodeGizmosColor);
		}
	}
}