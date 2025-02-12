using UnityEngine;

public class PlayersTotalDefeatedEnemiesCountersPanelUI : MonoBehaviour
{
	private PlayerTotalDefeatedEnemiesIntCounter[] playerTotalDefeatedEnemiesIntCounters;
	
	public void SetActive(bool active)
	{
		playerTotalDefeatedEnemiesIntCounters.ForEach(playerTotalDefeatedEnemiesIntCounter =>
		{
			if(active)
			{
				playerTotalDefeatedEnemiesIntCounter.ActivateIfPossible();
			}
			else
			{
				playerTotalDefeatedEnemiesIntCounter.Deactivate();
			}
		});
	}

	private void Awake()
	{
		playerTotalDefeatedEnemiesIntCounters = GetComponentsInChildren<PlayerTotalDefeatedEnemiesIntCounter>();

		SetActive(false);
	}
}