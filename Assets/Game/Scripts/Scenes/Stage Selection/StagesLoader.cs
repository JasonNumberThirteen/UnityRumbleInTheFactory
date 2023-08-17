using UnityEngine;

public class StagesLoader : MonoBehaviour
{
	public GameData gameData;

	private void Awake() => gameData.stages = DetectedStages();

	private Stage[] DetectedStages()
	{
		Object[] data = Resources.LoadAll("Stages", typeof(TextAsset));
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