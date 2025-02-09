using UnityEngine;

public class DefeatedEnemyTypesPanelUI : MonoBehaviour
{
	[SerializeField] private DefeatedEnemyTypeIntCounterPanelUI defeatedEnemyTypeIntCounterPanelUIPrefab;
	[SerializeField] private GameObject horizontalLineTextUIPrefab;
	[SerializeField] private GameObject playersTotalDefeatedEnemiesCountersPanelUIPrefab;

	private DefeatedEnemyTypeIntCounterPanelUI[] defeatedEnemyTypeIntCounterPanelUIs;

	public DefeatedEnemyTypeIntCounterPanelUI GetDefeatedEnemyTypeIntCounterPanelUIByIndex(int index) => defeatedEnemyTypeIntCounterPanelUIs[index];

	private void Awake()
	{
		SpawnAndSetupDefeatedEnemyTypePanelUIs();
		SpawnHorizontalLineTextUI();
		SpawnPlayersTotalDefeatedEnemiesCountersPanelUI();

		defeatedEnemyTypeIntCounterPanelUIs = GetComponentsInChildren<DefeatedEnemyTypeIntCounterPanelUI>();
	}

	private void SpawnAndSetupDefeatedEnemyTypePanelUIs()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer == null || defeatedEnemyTypeIntCounterPanelUIPrefab == null)
		{
			return;
		}

		var defeatedEnemiesFromAllPlayersKeys = playersDefeatedEnemiesSumContainer.DefeatedEnemies.Keys;
		
		foreach (var key in defeatedEnemiesFromAllPlayersKeys)
		{
			var enemyRobotSprite = key.GetDisplayInScoreSceneSprite();
			
			Instantiate(defeatedEnemyTypeIntCounterPanelUIPrefab, gameObject.transform).SetSprite(enemyRobotSprite);
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