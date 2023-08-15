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

	public int DefeatedEnemies
	{
		get
		{
			return defeatedEnemies;
		}
		set
		{
			defeatedEnemies = value;

			CheckEnemiesCount();
		}
	}

	private GameStates state = GameStates.ACTIVE;
	private int defeatedEnemies;

	private enum GameStates
	{
		ACTIVE, PAUSED, INTERRUPTED, WON, OVER
	}

	public void FreezeAllEnemies() => SetEnemiesFreeze(true);
	public void UnfreezeAllEnemies() => SetEnemiesFreeze(false);
	public GameObject[] FoundEnemies() => GameObject.FindGameObjectsWithTag(enemyTag);
	public void ResetDefeatedEnemiesByPlayer() => playerData.DefeatedEnemies.Clear();
	public void InitiatePlayerRespawn() => playerRespawnTimer.ResetTimer();
	public bool EnemiesAreFrozen() => freezeTimer.Started;
	public bool IsActive() => state == GameStates.ACTIVE;
	public bool IsPaused() => state == GameStates.PAUSED;
	public bool IsInterrupted() => state == GameStates.INTERRUPTED;
	public bool IsWon() => state == GameStates.WON;
	public bool IsOver() => state == GameStates.OVER;

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

		DisablePlayer();
	}

	public void DisablePlayer()
	{
		GameObject player = GameObject.FindGameObjectWithTag(playerTag);

		if(player != null)
		{
			if(player.TryGetComponent(out EntityMovement em))
			{
				em.Direction = Vector2.zero;

				Destroy(em);
			}

			if(player.TryGetComponent(out RobotShoot rs))
			{
				Destroy(rs);
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
		GameObject[] enemies = FoundEnemies();

		foreach (GameObject enemy in enemies)
		{
			if(enemy.TryGetComponent(out EnemyRobotFreeze erf))
			{
				if(freeze)
				{
					erf.Freeze();
				}
				else
				{
					erf.Unfreeze();
				}
			}
		}
	}
	
	private void Awake() => CheckSingleton();
	private bool WonTheGame() => DefeatedEnemies == enemySpawnManager.EnemiesCount() && enemySpawnManager.NoEnemiesLeft();

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

	private void CheckEnemiesCount()
	{
		if(WonTheGame())
		{
			state = GameStates.WON;

			sceneManagerTimer.StartTimer();
		}
	}
}