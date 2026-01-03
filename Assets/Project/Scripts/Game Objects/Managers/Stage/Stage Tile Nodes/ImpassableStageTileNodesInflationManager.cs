using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class ImpassableStageTileNodesInflationManager : MonoBehaviour
{
	[SerializeField, Min(0)] private int inflationThicknessInTiles = 1;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color inflationAreasGizmosColor = new(1f, 0f, 0f, 0.25f);
	[SerializeField] private Color inflatedImpassableStageTileNodesGizmosColor = Color.cyan;
	[SerializeField, Range(0.01f, 0.25f)] private float inflatedImpassableStageTileNodeGizmosSize = 0.2f;

	private StageTileNodesManager stageTileNodesManager;

	private static readonly float INFLATION_AREA_HORIZONTAL_SIZE_OFFSET = 0.1f;
	private static readonly float INFLATION_AREA_VERTICAL_SIZE = 0.25f;

	private readonly List<Rect> inflationAreas = new();
	private readonly List<StageTileNode> inflatedImpassableStageTileNodes = new();

	public List<StageTileNode> GetInflatedImpassableStageTileNodes() => inflatedImpassableStageTileNodes;

	private void Awake()
	{
		stageTileNodesManager = ObjectMethods.FindComponentOfType<StageTileNodesManager>();

		SetImpassableTilesInflation(inflationThicknessInTiles);
	}

	private void SetImpassableTilesInflation(int thicknessInTiles)
	{
		inflationThicknessInTiles = thicknessInTiles;

		if(stageTileNodesManager != null)
		{
			stageTileNodesManager.ResetStageTileNodes();
		}
		
		UpdateInflatedImpassableStageTileNodes();
	}

	private void UpdateInflatedImpassableStageTileNodes()
	{
		inflatedImpassableStageTileNodes.Clear();
		InflateImpassableStageTileNodes();
	}

	private void InflateImpassableStageTileNodes()
	{
		var impassableStageTileNodes = stageTileNodesManager != null ? stageTileNodesManager.GetStageTileNodes().Where(stageTileNode => !stageTileNode.Passable).ToList() : new List<StageTileNode>();

		impassableStageTileNodes?.ForEach(InflateImpassableStageTileNode);
	}

	private void InflateImpassableStageTileNode(StageTileNode stageTileNode)
	{
		var inflationAreas = GetInflationAreasAround(stageTileNode);

		inflationAreas.ForEach(this.inflationAreas.AddIfNotExists);
		inflationAreas.ForEach(AddInflatedImpassableStageTileNodesWithinArea);
	}

	private Rect[] GetInflationAreasAround(StageTileNode stageTileNode)
	{
		if(stageTileNode == null)
		{
			return new Rect[0];
		}
		
		var stageTileNodePosition = stageTileNode.GetPosition();
		var horizontalInflationAreaSize = new Vector2(inflationThicknessInTiles + INFLATION_AREA_HORIZONTAL_SIZE_OFFSET, INFLATION_AREA_VERTICAL_SIZE);
		var inflationAreas = new Rect[]
		{
			new(stageTileNodePosition.GetOffsetFrom(horizontalInflationAreaSize), horizontalInflationAreaSize),
			new(stageTileNodePosition.GetOffsetFrom(horizontalInflationAreaSize.GetInversedVector()), horizontalInflationAreaSize.GetInversedVector())
		};

		return inflationAreas;
	}

	private void AddInflatedImpassableStageTileNodesWithinArea(Rect area)
	{
		var passableStageTileNodesWithinArea = stageTileNodesManager != null ? stageTileNodesManager.GetStageTileNodesWithin(area).Where(stageTileNode => stageTileNode.Passable && area.Contains(stageTileNode.GetPosition())).ToList() : new List<StageTileNode>();

		passableStageTileNodesWithinArea.ForEach(inflatedImpassableStageTileNodes.AddIfNotExists);
	}

	private void OnDrawGizmos()
	{
		if(!drawGizmos)
		{
			return;
		}

		GizmosMethods.OperateOnGizmos(() => inflationAreas.ForEach(inflationArea => Gizmos.DrawCube(inflationArea.center, inflationArea.size)), inflationAreasGizmosColor);
		GizmosMethods.OperateOnGizmos(() => inflatedImpassableStageTileNodes.ForEach(inflatedImpassableStageTileNode => Gizmos.DrawCube(inflatedImpassableStageTileNode.transform.position, Vector2.one*inflatedImpassableStageTileNodeGizmosSize)), inflatedImpassableStageTileNodesGizmosColor);
	}
}