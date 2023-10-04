using UnityEngine;

public class StagesLoader : MonoBehaviour
{
	public LoopingCounter stageCounter;
	public GameData gameData;
	public string directory;

	private void Awake() => gameData.stages = DetectedStages();
	private void Start() => stageCounter.max = gameData.stages.Length;

	private Stage[] DetectedStages()
	{
		Object[] data = Resources.LoadAll(directory, typeof(TextAsset));
		int dataLength = data.Length;
		Stage[] stages = new Stage[dataLength];

		for (int i = 0; i < dataLength; ++i)
		{
			TextAsset ta = (TextAsset)data[i];
			
			stages[i] = JsonUtility.FromJson<Stage>(ta.text);
		}

		return stages;
	}
}

[System.Serializable]
public class Stage
{
	public int[] tiles, enemies;
}