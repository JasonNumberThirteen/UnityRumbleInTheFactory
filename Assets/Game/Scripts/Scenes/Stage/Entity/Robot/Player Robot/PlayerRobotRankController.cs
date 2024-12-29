using UnityEngine;

public class PlayerRobotRankController : MonoBehaviour
{
	public PlayerRobotRank CurrentRank {get; private set;}

	private PlayerRobotData data;
	
	public void Promote()
	{
		++data.Data.RankNumber;

		SetRank();
	}
	
	public void SetRank()
	{
		CurrentRank = data.Data.GetRank();

		UpdateValues();
	}

	private void Awake() => data = GetComponent<PlayerRobotData>();
	private void Start() => SetRank();

	private void UpdateValues()
	{
		IUpgradeableByRobotRank[] upgradeables = GetComponents<IUpgradeableByRobotRank>();

		foreach (IUpgradeableByRobotRank upgradeable in upgradeables)
		{
			upgradeable.UpdateValuesUpgradeableByPlayerRobotRank(CurrentRank);
		}
	}
}