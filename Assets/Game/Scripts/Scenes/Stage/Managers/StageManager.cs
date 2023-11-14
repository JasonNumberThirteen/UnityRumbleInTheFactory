using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	public string playerTag, playerSpawnerTag;
	[Min(0)] public int pointsForBonus = 500;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public EnemyFreezeManager enemyFreezeManager;
	public StageStateManager stateManager;
	public GameData gameData;
	public Timer gameOverTimer, sceneManagerTimer;

	private int defeatedEnemies;
	private PlayerData[] playersData;

	public void ResetDefeatedEnemiesByPlayer()
	{
		foreach (PlayerData pd in playersData)
		{
			pd.ResetDefeatedEnemies();
		}
	}

	public void CountDefeatedEnemy()
	{
		++defeatedEnemies;

		CheckIfWonTheGame();
	}

	public void AddPoints(GameObject go, PlayerData pd, int points)
	{
		pd.Score += points;

		uiManager.InstantiateGainedPointsCounter(go.transform.position, points);
	}

	public void CheckPlayersLives()
	{
		if(AllPlayersLostAllLives())
		{
			gameOverTimer.onEnd.Invoke();
		}
	}

	public void PauseGame()
	{
		if(stateManager.IsInterrupted() || stateManager.IsWon() || stateManager.IsOver())
		{
			return;
		}

		if(stateManager.IsActive())
		{
			stateManager.SetAsPaused();
		}
		else
		{
			stateManager.SetAsActive();
		}

		Time.timeScale = stateManager.IsPaused() ? 0f : 1f;

		uiManager.ControlPauseTextDisplay();
	}

	public void InterruptGame()
	{
		stateManager.SetAsInterrupted();
		gameOverTimer.StartTimer();
	}

	public void SetGameAsOver()
	{
		gameData.isOver = true;

		stateManager.SetAsOver();
		DisablePlayers();
	}

	public void DisablePlayers()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

		foreach (GameObject player in players)
		{
			if(player.TryGetComponent(out PlayerRobotDisabler prd))
			{
				prd.DisableYourself();
			}
		}
	}
	
	private void Awake()
	{
		CheckSingleton();
		DetectPlayers();
	}

	private bool WonTheGame() => DefeatedAllEnemies() && enemySpawnManager.NoEnemiesLeft();
	private bool DefeatedAllEnemies() => defeatedEnemies == enemySpawnManager.EnemiesCount();

	private void CheckSingleton()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void DetectPlayers()
	{
		GameObject[] spawners = GameObject.FindGameObjectsWithTag(playerSpawnerTag);

		playersData = new PlayerData[spawners.Length];

		for (int i = 0; i < spawners.Length; ++i)
		{
			if(spawners[i].TryGetComponent(out PlayerSpawner ps))
			{
				playersData[i] = ps.playerData;
			}
		}
	}

	private void CheckIfWonTheGame()
	{
		if(WonTheGame())
		{
			stateManager.SetAsWon();
			sceneManagerTimer.StartTimer();
		}
	}

	private bool AllPlayersLostAllLives()
	{
		foreach (PlayerData pd in playersData)
		{
			if(!pd.lostAllLives)
			{
				return false;
			}
		}

		return true;
	}
}