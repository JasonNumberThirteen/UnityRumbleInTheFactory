using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	public string playerTag, enemyTag;
	[Min(0)] public int pointsForBonus = 500;
	[Min(0.01f)] public float playerRespawnDelay = 1f;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public PlayerData playerData;
	public GameData gameData;
	public Timer gameOverTimer, freezeTimer, playerRespawnTimer, playerSpawnerTimer, sceneManagerTimer;

	private GameStates state = GameStates.ACTIVE;
	private int defeatedEnemies;

	private enum GameStates
	{
		ACTIVE, PAUSED, INTERRUPTED, WON, OVER
	}

	public void FreezeAllEnemies() => SetEnemiesFreeze(true);
	public void UnfreezeAllEnemies() => SetEnemiesFreeze(false);
	public GameObject[] FoundObjectsWithTag(string tag) => GameObject.FindGameObjectsWithTag(tag);
	public void ResetDefeatedEnemiesByPlayer() => playerData.DefeatedEnemies.Clear();
	public void InitiatePlayerRespawn() => playerRespawnTimer.ResetTimer();
	public bool GameIsOver() => IsInterrupted() || IsOver();
	public bool EnemiesAreFrozen() => freezeTimer.Started;
	public bool IsActive() => state == GameStates.ACTIVE;
	public bool IsPaused() => state == GameStates.PAUSED;
	public bool IsInterrupted() => state == GameStates.INTERRUPTED;
	public bool IsWon() => state == GameStates.WON;
	public bool IsOver() => state == GameStates.OVER;

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

	public void AttemptToRespawnPlayer()
	{
		if(playerData.Lives-- > 0)
		{
			playerData.OnRespawn();
			playerSpawnerTimer.ResetTimer();
		}
		else
		{
			gameOverTimer.onEnd.Invoke();
			InterruptGame();
		}
	}

	public void CheckPlayerLives()
	{
		if(playerData.Lives == 0)
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
	
	private void Awake() => CheckSingleton();
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

	private void CheckIfWonTheGame()
	{
		if(WonTheGame())
		{
			state = GameStates.WON;

			sceneManagerTimer.StartTimer();
		}
	}
}