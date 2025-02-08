using System;

[Serializable]
public class SaveablePlayerRobotData
{
	public int score;

	public void ResetData()
	{
		score = 0;
	}
}