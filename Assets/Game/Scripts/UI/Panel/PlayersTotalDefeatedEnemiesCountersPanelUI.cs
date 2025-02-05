using UnityEngine;

public class PlayersTotalDefeatedEnemiesCountersPanelUI : MonoBehaviour
{
	private PlayerTotalDefeatedEnemiesIntCounter[] playerTotalDefeatedEnemiesIntCounters;
	
	public void SetActive(bool active)
	{
		foreach (var playerTotalDefeatedEnemiesIntCounter in playerTotalDefeatedEnemiesIntCounters)
		{
			playerTotalDefeatedEnemiesIntCounter.gameObject.SetActive(active);
		}
	}

	private void Awake()
	{
		playerTotalDefeatedEnemiesIntCounters = GetComponentsInChildren<PlayerTotalDefeatedEnemiesIntCounter>();

		SetActive(false);
	}
}