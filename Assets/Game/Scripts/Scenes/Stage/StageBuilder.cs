using UnityEngine;

public class StageBuilder : MonoBehaviour
{
	public GameData gameData;
	public GameObject[] tiles;
	public Transform[] parents;
	public Vector2 positionOffset;

	private void Start() => BuildStage();

	private void BuildStage()
	{
		Stage stage = gameData.stages[gameData.stageNumber - 1];

		for (int i = 0; i < stage.tiles.Length; ++i)
		{
			int x = i % 26;
			int y = 26 - Mathf.FloorToInt(i / 26);
			int tileIndex = stage.tiles[i] - 1;

			if(tileIndex >= 0 && tileIndex < tiles.Length)
			{
				float tileX = x*0.5f;
				float tileY = y*0.5f;
				Vector2 tilePosition = new Vector2(tileX, tileY);
				
				Instantiate(tiles[tileIndex], tilePosition + positionOffset, Quaternion.identity, parents[tileIndex]);
			}
		}
	}
}