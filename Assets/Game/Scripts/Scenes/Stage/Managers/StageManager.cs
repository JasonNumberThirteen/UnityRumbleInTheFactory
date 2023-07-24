using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	[Min(0.01f)] public float playerRespawnDelay = 1f;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public PlayerData playerData;
	public Timer gameOverTimer, freezeTimer, playerRespawnTimer, playerSpawnerTimer, sceneManagerTimer;
	
	public GameStates State {get; private set;} = GameStates.ACTIVE;
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

	public enum GameStates
	{
		ACTIVE, PAUSED, INTERRUPTED, WON, OVER
	}

	public void InitiatePlayerRespawn() => playerRespawnTimer.ResetTimer();

	public void AttemptToRespawnPlayer()
	{
		if(playerData.Lives-- > 0)
		{
			playerData.Rank = 1;

			playerSpawnerTimer.ResetTimer();
		}
		else
		{
			gameOverTimer.onEnd.Invoke();
			InterruptGame();
		}
	}

	public void InterruptGame()
	{
		gameOverTimer.StartTimer();

		State = GameStates.INTERRUPTED;
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
		State = GameStates.OVER;

		DisablePlayer();
	}

	public void PauseGame()
	{
		if(State == GameStates.INTERRUPTED || State == GameStates.WON || State == GameStates.OVER)
		{
			return;
		}

		State = (State == GameStates.ACTIVE) ? GameStates.PAUSED : GameStates.ACTIVE;
		Time.timeScale = State == GameStates.PAUSED ? 0f : 1f;

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
	private void Start() => playerData.ResetData();

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
		if(DefeatedEnemies == enemySpawnManager.enemies.Length && enemySpawnManager.NoEnemiesLeft())
		{
			State = GameStates.WON;

			sceneManagerTimer.StartTimer();
		}
	}
}