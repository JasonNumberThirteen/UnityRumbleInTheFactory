using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNodesManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private Collider2D[] takenAreas;
	[SerializeField] private StageTileNode stageTileNodePrefab;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color impassableStageTileNodesGizmosColor = new(1f, 0f, 0f, 0.75f);
	[SerializeField, Range(0.01f, 0.25f)] private float impassableStageTileNodeGizmosSize = 0.2f;
	
	private StageLayoutManager stageLayoutManager;

	private readonly List<StageTileNode> stageTileNodes = new();
	private readonly List<StageTileType> prohibitedStageTileTypes = new() {StageTileType.ToxicWaste};
	private readonly List<StageTileType> impassableStageTileTypes = new() {StageTileType.Metal};

	public List<StageTileNode> GetStageTileNodes() => stageTileNodes;
	public List<StageTileNode> GetStageTileNodesWithin(Rect rect) => stageTileNodes.Where(stageTileNode => rect.Contains(stageTileNode.GetPosition())).ToList();
	public StageTileNode GetClosestStageTileNodeTo(Vector2 position) => stageTileNodes.OrderBy(stageTileNode => Vector2.Distance(stageTileNode.GetPosition(), position)).FirstOrDefault();

	public StageTileNode GetStageTileNodeWhereClosestPlayerRobotIsOnIfPossible(StageTileNode startStageTileNode)
	{
		var activePlayerRobotEntities = ObjectMethods.FindComponentsOfType<PlayerRobotEntity>(false);

		if(activePlayerRobotEntities == null || activePlayerRobotEntities.Length == 0)
		{
			return null;
		}

		var closestActivePlayerRobotEntity = activePlayerRobotEntities.OrderBy(playerRobot => Vector2.Distance(startStageTileNode.GetPosition(), playerRobot.transform.position)).FirstOrDefault();
		
		return GetClosestStageTileNodeTo(closestActivePlayerRobotEntity.gameObject.transform.position);
	}

	public void ResetStageTileNodes()
	{
		stageTileNodes.ForEach(stageTileNode => stageTileNode.ResetData());
	}

	private void Awake()
	{
		stageLayoutManager = ObjectMethods.FindComponentOfType<StageLayoutManager>();

		SpawnStageTileNodes();
		stageTileNodes.ForEach(stageTileNode => stageTileNode.FindNeighbours(stageTileNodes));
	}

	private void SpawnStageTileNodes()
	{
		var tileIndexes = GameDataMethods.GetTileIndexesFromCurrentStageData(gameData);
		
		tileIndexes.ForEachIndexed(SpawnStageTileNodeIfPossible);
	}

	private void SpawnStageTileNodeIfPossible(int tileIndex, int loopIndex)
	{
		var stageTileType = tileIndex.ToEnumValue<StageTileType>();
		
		if(stageLayoutManager == null || prohibitedStageTileTypes.Contains(stageTileType))
		{
			return;
		}

		var tilePosition = stageLayoutManager.GetTilePosition(loopIndex);

		if(!tilePosition.OverlapsWithAnyOfColliders(takenAreas))
		{
			SpawnStageTileNode(tilePosition, stageTileType);
		}
	}

	private void SpawnStageTileNode(Vector2 tilePosition, StageTileType stageTileType)
	{
		if(stageTileNodePrefab == null)
		{
			return;
		}
		
		var instance = Instantiate(stageTileNodePrefab, tilePosition, Quaternion.identity);

		instance.Passable = !impassableStageTileTypes.Contains(stageTileType);

		stageTileNodes.Add(instance);
	}

	private void OnDrawGizmos()
	{
		if(!drawGizmos)
		{
			return;
		}
		
		var impassableStageTileNodes = stageTileNodes.Where(stageTileNode => !stageTileNode.Passable).ToList();
		
		GizmosMethods.OperateOnGizmos(() => impassableStageTileNodes.ForEach(impassableStageTileNode => Gizmos.DrawCube(impassableStageTileNode.transform.position, Vector2.one*impassableStageTileNodeGizmosSize)), impassableStageTileNodesGizmosColor);
	}
}