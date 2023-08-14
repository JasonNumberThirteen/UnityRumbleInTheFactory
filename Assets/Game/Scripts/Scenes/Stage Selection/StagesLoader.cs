using UnityEngine;

public class StagesLoader : MonoBehaviour
{
	public GameData gameData;

	private Stage[] stages;

	public int DetectedStages() => stages.Length;
	public Stage StageByIndex(int index) => stages[index];

	private void Start() => DetectStages();

	private void DetectStages()
	{
		Object[] stagesData = Resources.LoadAll("Stages", typeof(TextAsset));
		
		stages = new Stage[stagesData.Length];

		for (int i = 0; i < stagesData.Length; ++i)
		{
			TextAsset ta = (TextAsset)stagesData[i];
			
			stages[i] = JsonUtility.FromJson<Stage>(ta.text);
		}

		gameData.stages = stages;
	}
}

[System.Serializable]
public class Stage
{
	public int[] tiles, enemies;
}