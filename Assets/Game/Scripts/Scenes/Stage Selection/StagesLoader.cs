using UnityEngine;

public class StagesLoader : MonoBehaviour
{
	public TextAsset[] stagesData;
	public GameData gameData;

	private Stage[] stages;

	public int DetectedStages() => stages.Length;
	public Stage StageByIndex(int index) => stages[index];

	private void Start() => DetectStages();

	private void DetectStages()
	{
		stages = new Stage[stagesData.Length];

		for (int i = 0; i < stagesData.Length; ++i)
		{
			stages[i] = JsonUtility.FromJson<Stage>(stagesData[i].text);
		}

		gameData.stages = stages;
	}
}

[System.Serializable]
public class Stage
{
	public int[] tiles, enemies;
}