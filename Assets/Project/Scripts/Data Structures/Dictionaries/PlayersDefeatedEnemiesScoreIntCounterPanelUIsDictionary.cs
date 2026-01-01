using System.Collections.Generic;

public class PlayersDefeatedEnemiesScoreIntCounterPanelUIsDictionary : Dictionary<PlayerRobotData, DefeatedEnemiesScoreIntCounterPanelUI>
{
	public void UpdatePanelUIs(List<PlayerScoreDetailsPanelUI> playerScoreDetailsPanelUIs, int index)
	{
		Clear();
		playerScoreDetailsPanelUIs.ForEach(playerScoreDetailsPanelUI => AddPanelUI(playerScoreDetailsPanelUI, index));
	}

	public void SetValueToCounterIfExists(PlayerRobotScoreData playerRobotScoreData)
	{
		if(TryGetValue(playerRobotScoreData.PlayerRobotData, out var defeatedEnemiesScoreIntCounterPanelUI))
		{
			defeatedEnemiesScoreIntCounterPanelUI.SetValueToCounter(playerRobotScoreData.CurrentScoreForDefeatedEnemyRobots);
		}
	}

	private void AddPanelUI(PlayerScoreDetailsPanelUI playerScoreDetailsPanelUI, int index)
	{
		if(playerScoreDetailsPanelUI == null)
		{
			return;
		}
		
		var playerRobotData = playerScoreDetailsPanelUI.GetPlayerRobotData();
		var defeatedEnemiesScoreIntCounterPanelUI = playerScoreDetailsPanelUI.GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(index);

		Add(playerRobotData, defeatedEnemiesScoreIntCounterPanelUI);
	}
}