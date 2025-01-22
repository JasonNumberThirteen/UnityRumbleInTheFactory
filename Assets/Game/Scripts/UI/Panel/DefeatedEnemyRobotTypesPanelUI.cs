using UnityEngine;

public class DefeatedEnemyRobotTypesPanelUI : MonoBehaviour
{
	[SerializeField] private DefeatedEnemyRobotTypeIntCounterPanelUI defeatedEnemyRobotTypeIntCounterPanelUIPrefab;
	[SerializeField] private GameObject horizontalLineTextUIPrefab;
	[SerializeField] private GameObject playersTotalDefeatedEnemiesCountersPanelUIPrefab;

	private DefeatedEnemyRobotTypeIntCounterPanelUI[] defeatedEnemyRobotTypeIntCounterPanelUIs;

	public DefeatedEnemyRobotTypeIntCounterPanelUI GetDefeatedEnemyRobotTypeIntCounterPanelUIByIndex(int index) => defeatedEnemyRobotTypeIntCounterPanelUIs[index];

	private void Awake()
	{
		SpawnAndSetupDefeatedEnemyRobotTypePanelUIs();
		SpawnHorizontalLineTextUI();
		SpawnPlayersTotalDefeatedEnemiesCountersPanelUI();

		defeatedEnemyRobotTypeIntCounterPanelUIs = GetComponentsInChildren<DefeatedEnemyRobotTypeIntCounterPanelUI>();
	}

	private void SpawnAndSetupDefeatedEnemyRobotTypePanelUIs()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer == null || defeatedEnemyRobotTypeIntCounterPanelUIPrefab == null)
		{
			return;
		}

		var defeatedEnemiesFromAllPlayersKeys = playersDefeatedEnemiesSumContainer.DefeatedEnemies.Keys;
		
		foreach (var key in defeatedEnemiesFromAllPlayersKeys)
		{
			var enemyRobotSprite = key.GetDisplayInScoreSceneSprite();
			
			Instantiate(defeatedEnemyRobotTypeIntCounterPanelUIPrefab, gameObject.transform).SetSprite(enemyRobotSprite);
		}
	}

	private void SpawnHorizontalLineTextUI()
	{
		if(horizontalLineTextUIPrefab != null)
		{
			Instantiate(horizontalLineTextUIPrefab, gameObject.transform);
		}
	}

	private void SpawnPlayersTotalDefeatedEnemiesCountersPanelUI()
	{
		if(playersTotalDefeatedEnemiesCountersPanelUIPrefab != null)
		{
			Instantiate(playersTotalDefeatedEnemiesCountersPanelUIPrefab, gameObject.transform);
		}
	}
}