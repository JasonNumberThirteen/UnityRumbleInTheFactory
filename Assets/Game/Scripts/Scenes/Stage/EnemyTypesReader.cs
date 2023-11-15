using UnityEngine;

public class EnemyTypesReader : MonoBehaviour
{
	public EnemyData[] enemiesData;
	public GameData gameData;

	public EnemyType[] EnemyTypes {get; private set;}

	private GameObject[] enemies;
	
	private void Awake() => AssignEnemiesFromCurrentStage();

	private void AssignEnemiesFromCurrentStage()
	{
		EnemyTypes = EnemiesTypes();

		int count = EnemyTypes.Length;
		
		enemies = new GameObject[count];

		for (int i = 0; i < count; ++i)
		{
			int index = EnemyTypes[i].index;
			
			enemies[i] = enemiesData[index].prefab;
		}
	}

	private EnemyType[] EnemiesTypes()
	{
		Stage stage = gameData.CurrentStage();
		int length = stage.enemies.Length;
		EnemyType[] types = new EnemyType[length];
		
		for (int i = 0; i < length; ++i)
		{
			string data = stage.enemies[i];
			int index = EnemyIndex(data);
			bool isBonusType = EnemyIsBonusType(data);
			
			types[i] = new EnemyType(index, isBonusType);
		}

		return types;
	}

	private int EnemyIndex(string data) => EnemyDataPointsToBonusType(data) ? int.Parse(data[1..]) : int.Parse(data);
	private bool EnemyIsBonusType(string data) => EnemyDataPointsToBonusType(data);
	private bool EnemyDataPointsToBonusType(string data) => data.StartsWith("B");
}