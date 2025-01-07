using UnityEngine;

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

	private void Start()
	{
		if(gameData == null)
		{
			return;
		}

		var currentStageData = gameData.GetCurrentStageData();
		var tileIndexes = currentStageData != null ? currentStageData.tileIndexes : new int[0];
		
		BuildStageLayout(tileIndexes);
	}

	private void BuildStageLayout(int[] tilesIndexes)
	{
		for (int i = 0; i < tilesIndexes.Length; ++i)
		{
			SpawnTile(i, tilesIndexes[i]);
		}
	}

	private void SpawnTile(int loopIndex, int tileIndex)
	{
		if(!IndexIsWithinTilesLength(tileIndex))
		{
			return;
		}
		
		var boardPosition = GetBoardPosition(loopIndex);
		var tilePosition = GetTilePosition(boardPosition);
		var tilePrefab = tilesPrefabs[tileIndex];

		if(tilePrefab != null && TileCanBePlaced(tilePosition))
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

	private bool TileCanBePlaced(Vector2 position)
	{
		if(takenAreas == null)
		{
			return false;
		}
		
		foreach (var takenArea in takenAreas)
		{
			if(takenArea != null && takenArea.OverlapPoint(position))
			{
				return false;
			}
		}

		return true;
	}

	private void OnDrawGizmos()
	{
		if(!drawGizmos || takenAreas == null)
		{
			return;
		}
		
		GizmosMethods.OperateOnGizmos(() =>
		{
			foreach (var takenArea in takenAreas)
			{
				if(takenArea != null)
				{
					Gizmos.DrawWireCube(takenArea.bounds.center, takenArea.bounds.size);
				}
			}
		}, prohibitedAreasGizmosColor);
	}
	
	private bool IndexIsWithinTilesLength(int index) => index >= 0 && index < tilesPrefabs.Length;
	private int GetBoardPositionX(int index) => index % stageWidthInTiles;
	private int GetBoardPositionY(int index) => stageHeightInTiles - GetBoardPositionOffsetY(index);
	private int GetBoardPositionOffsetY(int index) => Mathf.FloorToInt(index / stageHeightInTiles);
	private Vector2 GetTilePosition(Vector2 position) => position*tileSize + positionOffset;
}