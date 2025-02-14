using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StageTileNodesManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private Collider2D[] takenAreas;
	[SerializeField] private TileNode tileNodePrefab;
	
	private StageLayoutManager stageLayoutManager;

	private readonly List<TileNode> tileNodes = new();
	private readonly List<StageTileType> prohibitedTileTypes = new() {StageTileType.ToxicWaste};
	private readonly List<StageTileType> impassableTileTypes = new() {StageTileType.Metal};

	public List<TileNode> GetTileNodesWithin(Rect rect) => tileNodes.Where(tileNode => rect.Contains(tileNode.GetPosition())).ToList();
	public TileNode GetClosestTileNodeTo(Vector2 position) => tileNodes.OrderBy(tileNode => Vector2.Distance(tileNode.GetPosition(), position)).FirstOrDefault();

	public TileNode GetTileNodeWhereClosestPlayerRobotIsOnIfPossible(TileNode startTileNode)
	{
		var activePlayerRobotEntities = ObjectMethods.FindComponentsOfType<PlayerRobotEntity>(false);

		if(activePlayerRobotEntities == null || activePlayerRobotEntities.Length == 0)
		{
			return null;
		}

		var closestActivePlayerRobotEntity = activePlayerRobotEntities.OrderBy(playerRobot => Vector2.Distance(startTileNode.GetPosition(), playerRobot.transform.position)).FirstOrDefault();
		
		return GetClosestTileNodeTo(closestActivePlayerRobotEntity.gameObject.transform.position);
	}

	public void ResetTileNodes()
	{
		tileNodes.ForEach(tileNode => tileNode.ResetData());
	}

	private void Awake()
	{
		stageLayoutManager = ObjectMethods.FindComponentOfType<StageLayoutManager>();

		SpawnTileNodes(GetTileIndexesFromCurrentStageDataIfPossible());
		tileNodes.ForEach(tileNode => tileNode.FindNeighbors(tileNodes));
	}

	private int[] GetTileIndexesFromCurrentStageDataIfPossible()
	{
		if(gameData == null)
		{
			return new int[0];
		}
		
		var currentStageData = gameData.GetCurrentStageData();

		return currentStageData != null ? currentStageData.GetTileIndexes() : new int[0];
	}

	private void SpawnTileNodes(int[] tilesIndexes)
	{
		for (int i = 0; i < tilesIndexes.Length; ++i)
		{
			SpawnTileNodeIfNeeded(i, tilesIndexes[i]);
		}
	}

	private void SpawnTileNodeIfNeeded(int loopIndex, int tileIndex)
	{
		var tileType = (StageTileType)tileIndex;
		
		if(stageLayoutManager == null || prohibitedTileTypes.Contains(tileType))
		{
			return;
		}

		var tilePosition = stageLayoutManager.GetTilePosition(loopIndex);

		if(!tilePosition.OverlapsWithAnyOfColliders(takenAreas))
		{
			SpawnTileNode(tilePosition, tileType);
		}
	}

	private void SpawnTileNode(Vector2 tilePosition, StageTileType stageTileType)
	{
		if(tileNodePrefab == null)
		{
			return;
		}
		
		var instance = Instantiate(tileNodePrefab, tilePosition, Quaternion.identity);

		instance.Passable = !impassableTileTypes.Contains(stageTileType);

		tileNodes.Add(instance);
	}
}