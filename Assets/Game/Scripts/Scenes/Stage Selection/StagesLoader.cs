using UnityEngine;

public class StagesLoader : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private readonly string STAGES_DATA_PATH = "Stages";

	private void Awake()
	{
		if(gameData != null)
		{
			gameData.stages = GetStagesDataFromPath(STAGES_DATA_PATH);
		}
	}

	private StageData[] GetStagesDataFromPath(string path)
	{
		var assets = Resources.LoadAll(path, typeof(TextAsset));
		var numberOfAssets = assets.Length;
		var stagesData = new StageData[numberOfAssets];

		for (var i = 0; i < numberOfAssets; ++i)
		{
			stagesData[i] = GetStageDataFromAsset(assets[i]);
		}

		return stagesData;
	}

	private StageData GetStageDataFromAsset(Object asset)
	{
		if(asset is TextAsset textAsset)
		{
			return JsonUtility.FromJson<StageData>(textAsset.text);
		}

		return null;
	}
}