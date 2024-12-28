using UnityEngine;

public class PlayerScoreIntCounter : IntCounter
{
	[SerializeField] private PlayerData playerData;

	private void Start()
	{
		if(playerData != null)
		{
			SetTo(playerData.Score);
		}
	}
}