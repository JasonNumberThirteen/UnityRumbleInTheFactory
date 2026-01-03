using UnityEngine;

[DefaultExecutionOrder(-100)]
public class StageLayoutManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private GameObject[] tilesPrefabs;
	[SerializeField] private Vector2 positionOffset = new(-6.75f, -6.75f);
	[SerializeField, Min(1)] private int stageWidthInTiles = 26;
	[SerializeField, Min(1)] private int stageHeightInTiles = 26;
	[SerializeField, Min(0.01f)] private float tileSize = 0.5f;
	[SerializeField] private Collider2D[] takenAreas;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color prohibitedAreasGizmosColor = Color.red;

	public float GetTileSize() => tileSize;

	public Vector2 GetTilePosition(int loopIndex)
	{
		var boardPosition = GetBoardPosition(loopIndex);

		return boardPosition*tileSize + positionOffset;
	}

	private void Awake()
	{
		var tileIndexes = GameDataMethods.GetTileIndexesFromCurrentStageData(gameData);

		tileIndexes.ForEachIndexed(SpawnTileIfPossible);
	}

	private void SpawnTileIfPossible(int tileIndex, int loopIndex)
	{
		if(!IndexIsWithinTilesLength(tileIndex))
		{
			return;
		}
		
		var tilePrefab = tilesPrefabs[tileIndex];
		var tilePosition = GetTilePosition(loopIndex);

		if(tilePrefab != null && !tilePosition.OverlapsWithAnyOfColliders(takenAreas))
		{
			Instantiate(tilePrefab, tilePosition, Quaternion.identity);
		}
	}

	private Vector2 GetBoardPosition(int index)
	{
		var x = GetBoardPositionX(index);
		var y = GetBoardPositionY(index);
		
		return new(x, y);
	}

	private void OnDrawGizmos()
	{
		if(!drawGizmos || takenAreas == null)
		{
			return;
		}
		
		GizmosMethods.OperateOnGizmos(() =>
		{
			takenAreas.ForEach(takenArea =>
			{
				if(takenArea != null)
				{
					Gizmos.DrawWireCube(takenArea.bounds.center, takenArea.bounds.size);
				}
			});
		}, prohibitedAreasGizmosColor);
	}
	
	private bool IndexIsWithinTilesLength(int index) => index >= 0 && index < tilesPrefabs.Length;
	private int GetBoardPositionX(int index) => index % stageWidthInTiles;
	private int GetBoardPositionY(int index) => stageHeightInTiles - GetBoardPositionOffsetY(index);
	private int GetBoardPositionOffsetY(int index) => Mathf.FloorToInt(index / stageHeightInTiles);
}