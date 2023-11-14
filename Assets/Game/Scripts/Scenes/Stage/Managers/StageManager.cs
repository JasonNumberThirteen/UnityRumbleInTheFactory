using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	public string playerTag, playerSpawnerTag;
	[Min(0)] public int pointsForBonus = 500;
	public StageUIManager uiManager;
	public StageStateManager stateManager;
	public EnemySpawnManager enemySpawnManager;
	public EnemyFreezeManager enemyFreezeManager;
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

		stateManager.SwitchPauseState();
		uiManager.ControlPauseTextDisplay();

		Time.timeScale = stateManager.IsPaused() ? 0f : 1f;
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
		GameObject[] players = FoundObjectsWithTag(playerTag);

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
		FindPlayers();
	}

	private GameObject[] FoundObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);
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

	private void FindPlayers()
	{
		GameObject[] spawners = FoundObjectsWithTag(playerSpawnerTag);
		int length = spawners.Length;

		playersData = new PlayerData[length];

		for (int i = 0; i < length; ++i)
		{
			if(spawners[i].TryGetComponent(out PlayerSpawner ps))
			{
				playersData[i] = ps.playerData;
			}
		}
	}

	private void CheckIfWonTheGame()
	{
		if(!WonTheGame())
		{
			return;
		}

		stateManager.SetAsWon();
		sceneManagerTimer.StartTimer();
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