using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatedEnemyRobotTypeIntCounterPanelUI : MonoBehaviour
{
	private Image image;
	private PlayerRobotDataIntCounter[] playerRobotDataIntCounters;
	
	public void SetSprite(Sprite sprite)
	{
		if(image != null)
		{
			image.sprite = sprite;
		}
	}

	public void SetValuesToCountersIfPossible(List<PlayerRobotScoreData> playerRobotScoreDataList)
	{
		if(playerRobotDataIntCounters != null && playerRobotScoreDataList != null)
		{
			playerRobotScoreDataList.ForEach(SetValueToAppropriateIntCounterIfPossible);
		}
	}

	private void Awake()
	{
		image = GetComponentInChildren<Image>();
		playerRobotDataIntCounters = GetComponentsInChildren<PlayerRobotDataIntCounter>();
	}

	private void SetValueToAppropriateIntCounterIfPossible(PlayerRobotScoreData playerRobotScoreData)
	{
		var playerRobotDataIntCounter = playerRobotDataIntCounters.FirstOrDefault(playerRobotDataIntCounter => playerRobotDataIntCounter.GetPlayerRobotData() == playerRobotScoreData.PlayerRobotData);

		if(playerRobotDataIntCounter != null)
		{
			playerRobotDataIntCounter.SetTo(playerRobotScoreData.NumberOfCountedEnemyRobots);
		}
	}
}