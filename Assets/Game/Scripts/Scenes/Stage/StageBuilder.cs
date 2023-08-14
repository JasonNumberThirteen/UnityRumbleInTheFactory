using UnityEngine;

public class StageBuilder : MonoBehaviour
{
	public GameData gameData;
	public GameObject[] tiles;
	public Vector2 positionOffset;
	[Min(1)] public int stageWidthInTiles, stageHeightInTiles;
	[Min(0.01f)] public float tileSize;

	private void Start() => BuildStage();
	private Vector2 BoardPosition(int index) => new Vector2(BoardPositionX(index), BoardPositionY(index));
	private int BoardPositionX(int index) => index % stageWidthInTiles;
	private int BoardPositionY(int index) => stageHeightInTiles - BoardPositionOffsetY(index);
	private int BoardPositionOffsetY(int index) => Mathf.FloorToInt(index / stageHeightInTiles);
	private Vector2 TilePosition(Vector2 position) => position*tileSize + positionOffset;
	private bool TileExistsOnTheIndex(int index) => index >= 0 && index < tiles.Length;

	private void BuildStage()
	{
		int[] tilesIndexes = gameData.CurrentStage().tiles;

		for (int i = 0; i < tilesIndexes.Length; ++i)
		{
			InstantiateTile(i, tilesIndexes[i] - 1);
		}
	}

	private void InstantiateTile(int loopIndex, int tileIndex)
	{
		if(TileExistsOnTheIndex(tileIndex))
		{
			Vector2 position = TilePosition(BoardPosition(loopIndex));
			
			Instantiate(tiles[tileIndex], position, Quaternion.identity);
		}
	}
}