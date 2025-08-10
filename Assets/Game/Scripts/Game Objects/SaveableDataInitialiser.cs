using UnityEngine;

public class SaveableDataInitialiser : MonoBehaviour
{
	[SerializeField] private GameData gameData;
	[SerializeField] private PlayerRobotsListData playerRobotsListData;

	public void InitSaveableData()
	{
		if(GameDataMethods.GameDataIsDefined(gameData))
		{
			gameData.InitSaveableGameData();
		}

		if(playerRobotsListData != null)
		{
			playerRobotsListData.InitAllSaveablePlayerData();
		}
	}
}