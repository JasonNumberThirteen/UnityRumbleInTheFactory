using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class StagesDataLoadingManager : MonoBehaviour
{
	[SerializeField] private GameData gameData;

	private readonly string STAGES_DATA_PATH = "Stages";

	private void Awake()
	{
		if(gameData != null)
		{
			gameData.SetStagesData(GetStagesDataFromPath(STAGES_DATA_PATH));
		}
	}

	private StageData[] GetStagesDataFromPath(string path)
	{
		var assets = Resources.LoadAll(path, typeof(TextAsset)).OrderBy(GetStageNumberFromFilename).ToArray();
		var numberOfAssets = assets.Length;
		var stagesData = new StageData[numberOfAssets];

		stagesData.ForEachIndexed((stageData, i) => stagesData[i] = GetStageDataFromAsset(assets[i]));

		return stagesData;
	}

	private int GetStageNumberFromFilename(Object asset)
	{
		var match = Regex.Match(asset.name, @"\d+");
		
		return int.TryParse(match.Value, out var stageNumber) ? stageNumber : int.MaxValue;
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