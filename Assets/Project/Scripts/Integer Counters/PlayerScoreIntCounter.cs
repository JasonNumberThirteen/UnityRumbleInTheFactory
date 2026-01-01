using UnityEngine;

public class PlayerScoreIntCounter : IntCounter
{
	[SerializeField] private PlayerRobotData playerRobotData;

	private void Start()
	{
		if(playerRobotData != null)
		{
			SetTo(playerRobotData.Score);
		}
	}
}