using UnityEngine;

public class StagesLoader : MonoBehaviour
{
	public LoopingCounter stageCounter;
	public GameData gameData;
	public string directory;

	private void Awake() => gameData.stages = DetectedStages();
	private void Start() => stageCounter.max = gameData.stages.Length;

	private StageData[] DetectedStages()
	{
		Object[] data = Resources.LoadAll(directory, typeof(TextAsset));
		int dataLength = data.Length;
		StageData[] stages = new StageData[dataLength];

		for (int i = 0; i < dataLength; ++i)
		{
			TextAsset ta = (TextAsset)data[i];
			
			stages[i] = JsonUtility.FromJson<StageData>(ta.text);
		}

		return stages;
	}
}