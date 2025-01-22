using System.Linq;
using UnityEngine;

public class PlayerScoreDetailsPanelUI : MonoBehaviour
{
	[SerializeField] private GameObject defeatedEnemiesScoreCounterPanelUIPrefab;

	private DefeatedEnemiesScoreIntCounterPanelUI[] defeatedEnemiesScoreIntCounterPanelUIs;

	public DefeatedEnemiesScoreIntCounterPanelUI GetDefeatedEnemiesScoreIntCounterPanelUIByIndex(int index) => defeatedEnemiesScoreIntCounterPanelUIs[index];

	private void Awake()
	{
		var playersDefeatedEnemiesSumContainer = ObjectMethods.FindComponentOfType<PlayersDefeatedEnemiesSumContainer>();

		if(playersDefeatedEnemiesSumContainer == null || defeatedEnemiesScoreCounterPanelUIPrefab == null)
		{
			return;
		}

		playersDefeatedEnemiesSumContainer.DefeatedEnemies.ToList().ForEach(pair => Instantiate(defeatedEnemiesScoreCounterPanelUIPrefab, gameObject.transform));

		defeatedEnemiesScoreIntCounterPanelUIs = GetComponentsInChildren<DefeatedEnemiesScoreIntCounterPanelUI>();
	}
}