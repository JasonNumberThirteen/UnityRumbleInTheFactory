using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	public string playerTag, enemyTag;
	[Min(0)] public int pointsForBonus = 500;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public GameData gameData;
	public Timer gameOverTimer, freezeTimer, sceneManagerTimer;

	private GameStates state = GameStates.ACTIVE;
	private int defeatedEnemies;
	private PlayerData[] playersData;

	private enum GameStates
	{
		ACTIVE, PAUSED, INTERRUPTED, WON, OVER
	}

	public void FreezeAllEnemies() => SetEnemiesFreeze(true);
	public void UnfreezeAllEnemies() => SetEnemiesFreeze(false);
	public GameObject[] FoundObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);
	public bool GameIsOver() => IsInterrupted() || IsOver();
	public bool EnemiesAreFrozen() => freezeTimer.Started;
	public bool IsActive() => state == GameStates.ACTIVE;
	public bool IsPaused() => state == GameStates.PAUSED;
	public bool IsInterrupted() => state == GameStates.INTERRUPTED;
	public bool IsWon() => state == GameStates.WON;
	public bool IsOver() => state == GameStates.OVER;

	public void ResetDefeatedEnemiesByPlayer()
	{
		foreach (PlayerData pd in playersData)
		{
			pd.DefeatedEnemies.Clear();
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
			InterruptGame();
		}
	}

	public void PauseGame()
	{
		if(IsInterrupted() || IsWon() || IsOver())
		{
			return;
		}

		state = IsActive() ? GameStates.PAUSED : GameStates.ACTIVE;
		Time.timeScale = IsPaused() ? 0f : 1f;

		uiManager.ControlPauseTextDisplay();
	}

	public void InterruptGame()
	{
		state = GameStates.INTERRUPTED;
		
		gameOverTimer.StartTimer();
	}

	public void SetGameAsOver()
	{
		state = GameStates.OVER;
		gameData.isOver = true;

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

	public void InitiateFreeze(float duration)
	{
		freezeTimer.duration = duration;

		freezeTimer.ResetTimer();
	}

	public void SetEnemiesFreeze(bool freeze)
	{
		GameObject[] enemies = FoundObjectsWithTag(enemyTag);

		foreach (GameObject enemy in enemies)
		{
			if(enemy.TryGetComponent(out EnemyRobotFreeze erf))
			{
				erf.SetFreezeState(freeze);
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
		GameObject[] spawners = GameObject.FindGameObjectsWithTag("Player Spawner");

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
			state = GameStates.WON;

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