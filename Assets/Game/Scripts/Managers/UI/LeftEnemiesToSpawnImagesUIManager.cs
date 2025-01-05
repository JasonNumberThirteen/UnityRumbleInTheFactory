using UnityEngine;

public class LeftEnemiesToSpawnImagesUIManager : MonoBehaviour
{
	[SerializeField] private GameObject leftEnemyToSpawnImageUIPrefab;
	[SerializeField, Min(0)] private int maxIconsLimit = 20;
	
	private LeftEnemiesToSpawnPanelUI leftEnemiesToSpawnPanelUI;
	private EnemyRobotEntitySpawnManager enemyRobotEntitySpawnManager;
	private GameObject[] iconGOs;
	private int currentIconIndex;

	public void DestroyNextIconIfPossible()
	{
		if(--currentIconIndex < iconGOs.Length)
		{
			Destroy(iconGOs[currentIconIndex]);
		}
	}

	private void Awake()
	{
		leftEnemiesToSpawnPanelUI = FindAnyObjectByType<LeftEnemiesToSpawnPanelUI>(FindObjectsInactive.Include);
		enemyRobotEntitySpawnManager = FindAnyObjectByType<EnemyRobotEntitySpawnManager>(FindObjectsInactive.Include);
	}
	
	private void Start()
	{
		var numberOfEnemies = enemyRobotEntitySpawnManager != null ? enemyRobotEntitySpawnManager.GetTotalNumberOfEnemies() : 0;
		var numberOfIcons = GetNumberOfIconsToSpawn(numberOfEnemies);

		iconGOs = new GameObject[numberOfIcons];
		currentIconIndex = numberOfEnemies;

		for (int i = 0; i < numberOfIcons; ++i)
		{
			iconGOs[i] = GetIconInstance();
		}
	}

	private GameObject GetIconInstance()
	{
		var transform = leftEnemiesToSpawnPanelUI != null ? leftEnemiesToSpawnPanelUI.transform : null;
		
		return Instantiate(leftEnemyToSpawnImageUIPrefab, transform);
	}

	private int GetNumberOfIconsToSpawn(int numberOfEnemies) => Mathf.Min(numberOfEnemies, maxIconsLimit);
}