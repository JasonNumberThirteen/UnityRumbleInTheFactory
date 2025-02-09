using System;

[Serializable]
public class SaveablePlayerData
{
	public int score;

	public void ResetData()
	{
		score = 0;
	}
}