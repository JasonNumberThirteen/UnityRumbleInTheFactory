using UnityEngine;

[RequireComponent(typeof(PlayerRobot))]
public class PlayerRobotRankController : MonoBehaviour
{
	public PlayerRobotRank CurrentRank {get; private set;}

	private PlayerRobot playerRobot;
	
	public void Promote()
	{
		++playerRobot.GetPlayerData().RankNumber;

		SetRank();
	}
	
	public void SetRank()
	{
		CurrentRank = playerRobot.GetPlayerData().GetRank();

		UpdateValues();
	}

	private void Awake() => playerRobot = GetComponent<PlayerRobot>();
	private void Start() => SetRank();

	private void UpdateValues()
	{
		IUpgradeableByRobotRank[] upgradeables = GetComponents<IUpgradeableByRobotRank>();

		foreach (IUpgradeableByRobotRank upgradeable in upgradeables)
		{
			upgradeable.UpdateValuesUpgradeableByRobotRank(CurrentRank);
		}
	}
}