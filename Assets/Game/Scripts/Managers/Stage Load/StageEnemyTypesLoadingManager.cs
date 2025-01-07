using UnityEngine;

public class StageEnemyTypesLoadingManager : MonoBehaviour
{
	public EnemyType[] EnemyTypes {get; private set;}
	public GameObject[] EnemyPrefabs {get; private set;}

	[SerializeField] private EnemyData[] enemiesData;
	[SerializeField] private GameData gameData;

	private readonly char ENEMY_BONUS_TYPE_PREFIX = 'B';
	
	private void Awake()
	{
		var stageData = gameData != null ? gameData.GetCurrentStageData() : null;
		
		EnemyTypes = GetEnemyTypesFromStageData(stageData);
		EnemyPrefabs = new GameObject[EnemyTypes.Length];
		
		AssignEnemyPrefabsToCurrentStage();
	}

	private void AssignEnemyPrefabsToCurrentStage()
	{
		for (var i = 0; i < EnemyTypes.Length; ++i)
		{
			var index = EnemyTypes[i].Index;
			
			EnemyPrefabs[i] = enemiesData[index].GetPrefab();
		}
	}

	private EnemyType[] GetEnemyTypesFromStageData(StageData stageData)
	{
		var enemyTypesData = stageData?.GetEnemyTypes();
		var numberOfEnemyTypesData = enemyTypesData != null ? enemyTypesData.Length : 0;
		var enemyTypes = new EnemyType[numberOfEnemyTypesData];
		
		for (int i = 0; i < numberOfEnemyTypesData; ++i)
		{
			var enemyTypeData = enemyTypesData[i];
			
			enemyTypes[i] = new EnemyType(GetEnemyIndex(enemyTypeData), EnemyDataPointsToBonusType(enemyTypeData));
		}

		return enemyTypes;
	}

	private int GetEnemyIndex(string dataString)
	{
		var dataToParse = EnemyDataPointsToBonusType(dataString) ? dataString[1..] : dataString;
		
		return int.Parse(dataToParse);
	}

	private bool EnemyDataPointsToBonusType(string dataString) => dataString.StartsWith(ENEMY_BONUS_TYPE_PREFIX);
}