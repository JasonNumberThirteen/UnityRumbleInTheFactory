using UnityEngine;

public class DefeatedEnemyTypesPanelUI : MonoBehaviour
{
	[SerializeField] private DefeatedEnemyTypePanelUI defeatedEnemyTypePanelUIPrefab;
	[SerializeField] private GameObject horizontalLineTextUIPrefab;
	[SerializeField] private GameObject playersTotalDefeatedEnemiesCountersPanelUIPrefab;

	private DefeatedEnemyTypePanelUI[] defeatedEnemyTypePanelUIs;

	public DefeatedEnemyTypePanelUI GetDefeatedEnemyTypePanelUIByIndex(int index) => defeatedEnemyTypePanelUIs[index];

	private void Awake()
	{
		SpawnAndSetupDefeatedEnemyTypePanelUIs();
		SpawnHorizontalLineTextUI();
		SpawnPlayersTotalDefeatedEnemiesCountersPanelUI();

		defeatedEnemyTypePanelUIs = GetComponentsInChildren<DefeatedEnemyTypePanelUI>();
	}

	private void SpawnAndSetupDefeatedEnemyTypePanelUIs()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer == null || defeatedEnemyTypePanelUIPrefab == null)
		{
			return;
		}

		var defeatedEnemiesFromAllPlayersKeys = playersDefeatedEnemiesSumContainer.TotalDefeatedEnemies.Keys;
		
		foreach (var key in defeatedEnemiesFromAllPlayersKeys)
		{
			var enemyRobotSprite = key.GetDisplayInScoreSceneSprite();
			
			Instantiate(defeatedEnemyTypePanelUIPrefab, gameObject.transform).SetSprite(enemyRobotSprite);
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