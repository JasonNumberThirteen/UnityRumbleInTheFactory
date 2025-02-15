using System.Linq;
using UnityEngine;

public class PlayerScoreDetailsPanelUI : MonoBehaviour
{
	[SerializeField, Min(1)] private int ordinalNumber;
	[SerializeField] private GameObject defeatedEnemiesScoreCounterPanelUIPrefab;
	[SerializeField] private PlayerRobotData playerRobotData;

	private DefeatedEnemiesScoreIntCounterPanelUI[] defeatedEnemiesScoreIntCounterPanelUIs;

	public int GetOrdinalNumber() => ordinalNumber;
	public PlayerRobotData GetPlayerRobotData() => playerRobotData;
	public DefeatedEnemiesScoreIntCounterPanelUI GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(int index) => defeatedEnemiesScoreIntCounterPanelUIs != null && index < defeatedEnemiesScoreIntCounterPanelUIs.Length ? defeatedEnemiesScoreIntCounterPanelUIs[index] : null;

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