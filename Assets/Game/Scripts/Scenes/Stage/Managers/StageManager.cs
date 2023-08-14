using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	[Min(0.01f)] public float playerRespawnDelay = 1f;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public PlayerData playerData;
	public GameData gameData;
	public Timer gameOverTimer, freezeTimer, playerRespawnTimer, playerSpawnerTimer, sceneManagerTimer;
	
	private GameStates state = GameStates.ACTIVE;

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

	private int defeatedEnemies;

	private enum GameStates
	{
		ACTIVE, PAUSED, INTERRUPTED, WON, OVER
	}

	public void InitiatePlayerRespawn() => playerRespawnTimer.ResetTimer();
	public bool IsActive() => state == GameStates.ACTIVE;
	public bool IsPaused() => state == GameStates.PAUSED;
	public bool IsInterrupted() => state == GameStates.INTERRUPTED;
	public bool IsWon() => state == GameStates.WON;
	public bool IsOver() => state == GameStates.OVER;

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

	public void ResetDefeatedEnemiesByPlayer() => playerData.DefeatedEnemies.Clear();

	public void InterruptGame()
	{
		gameOverTimer.StartTimer();

		state = GameStates.INTERRUPTED;
	}

	public void InitiateFreeze(float duration)
	{
		freezeTimer.duration = duration;

		freezeTimer.ResetTimer();
	}

	public void FreezeAllEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			EnemyRobotFreeze erf = enemy.GetComponent<EnemyRobotFreeze>();

			erf.Freeze();
		}
	}

	public void UnfreezeAllEnemies()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			EnemyRobotFreeze erf = enemy.GetComponent<EnemyRobotFreeze>();

			erf.Unfreeze();
		}
	}

	public bool EnemiesAreFrozen() => freezeTimer.Started;

	public void SetGameAsOver()
	{
		state = GameStates.OVER;
		gameData.isOver = true;

		DisablePlayer();
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

	public void DisablePlayer()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if(player != null)
		{
			EntityMovement em = player.GetComponent<EntityMovement>();
			RobotShoot rs = player.GetComponent<RobotShoot>();

			if(em != null)
			{
				em.Direction = Vector2.zero;

				Destroy(em);
			}

			if(rs != null)
			{
				Destroy(rs);
			}
		}
	}
	
	private void Awake() => CheckSingleton();

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
		if(DefeatedEnemies == enemySpawnManager.EnemiesCount() && enemySpawnManager.NoEnemiesLeft())
		{
			state = GameStates.WON;

			sceneManagerTimer.StartTimer();
		}
	}
}