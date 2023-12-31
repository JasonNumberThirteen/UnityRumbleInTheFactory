using UnityEngine;

public class EnemyTypesReader : MonoBehaviour
{
	public EnemyData[] enemiesData;
	public GameData gameData;

	public EnemyType[] EnemyTypes {get; private set;}
	public GameObject[] Enemies {get; private set;}
	
	private void Awake() => AssignEnemiesFromCurrentStage();
	private int EnemyIndex(string data) => EnemyDataPointsToBonusType(data) ? int.Parse(data[1..]) : int.Parse(data);
	private bool EnemyIsBonusType(string data) => EnemyDataPointsToBonusType(data);
	private bool EnemyDataPointsToBonusType(string data) => data.StartsWith("B");

	private void AssignEnemiesFromCurrentStage()
	{
		int length;
		
		EnemyTypes = ReadEnemyTypes();
		length = EnemyTypes.Length;
		Enemies = new GameObject[length];

		for (int i = 0; i < length; ++i)
		{
			int index = EnemyTypes[i].index;
			
			Enemies[i] = enemiesData[index].prefab;
		}
	}

	private EnemyType[] ReadEnemyTypes()
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
}

public class EnemyType
{
	public int index;
	public bool isBonus;

	public EnemyType(int index, bool isBonus)
	{
		this.index = index;
		this.isBonus = isBonus;
	}
}