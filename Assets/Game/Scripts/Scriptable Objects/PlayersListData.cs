using UnityEngine;

[CreateAssetMenu(menuName = "Game/Players List Data")]
public class PlayersListData : ScriptableObject
{
	[SerializeField] private PlayerData[] playersData;

	public void ResetPlayersData()
	{
		foreach (var playerData in playersData)
		{
			playerData.ResetData();
		}
	}
}