using UnityEngine;

public class StageBuilder : MonoBehaviour
{
	public GameData gameData;
	public GameObject[] tiles;
	public Vector2 positionOffset;
	[Min(1)] public int stageWidthInTiles, stageHeightInTiles;
	[Min(0.01f)] public float tileSize;

	private void Start() => BuildStage();
	private int BoardPositionX(int index) => index % stageWidthInTiles;
	private int BoardPositionY(int index) => stageHeightInTiles - BoardPositionOffsetY(index);
	private int BoardPositionOffsetY(int index) => Mathf.FloorToInt(index / stageHeightInTiles);
	private Vector2 TilePosition(Vector2 position) => position*tileSize + positionOffset;
	private bool TileExistsOnTheIndex(int index) => index >= 0 && index < tiles.Length;

	private void BuildStage()
	{
		Stage stage = gameData.stages[gameData.stageNumber - 1];
		int[] tilesIndexes = stage.tiles;

		for (int i = 0; i < tilesIndexes.Length; ++i)
		{
			int tileIndex = tilesIndexes[i] - 1;

			if(TileExistsOnTheIndex(tileIndex))
			{
				int boardX = BoardPositionX(i);
				int boardY = BoardPositionY(i);
				Vector2 boardPosition = new Vector2(boardX, boardY);
				Vector2 tilePosition = TilePosition(boardPosition);
				
				Instantiate(tiles[tileIndex], tilePosition, Quaternion.identity);
			}
		}
	}
}