using System.Linq;
using UnityEngine;

public class PlayerScoreDetailsPanelUI : MonoBehaviour
{
	[SerializeField] private GameObject defeatedEnemiesScoreCounterPanelUIPrefab;
	[SerializeField] private PlayerRobotData playerRobotData;

	private DefeatedEnemiesScoreIntCounterPanelUI[] defeatedEnemiesScoreIntCounterPanelUIs;

	public PlayerRobotData GetPlayerRobotData() => playerRobotData;
	public DefeatedEnemiesScoreIntCounterPanelUI GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(int index) => defeatedEnemiesScoreIntCounterPanelUIs.GetElementAt(index);

	private void Awake()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer == null || defeatedEnemiesScoreCounterPanelUIPrefab == null)
		{
			return;
		}

		playersDefeatedEnemiesSumContainer.TotalDefeatedEnemies.ToList().ForEach(pair => Instantiate(defeatedEnemiesScoreCounterPanelUIPrefab, gameObject.transform));

		defeatedEnemiesScoreIntCounterPanelUIs = GetComponentsInChildren<DefeatedEnemiesScoreIntCounterPanelUI>();
	}
}