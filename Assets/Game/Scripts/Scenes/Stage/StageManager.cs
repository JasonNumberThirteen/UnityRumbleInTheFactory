using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	[Min(0.01f)] public float playerRespawnDelay = 1f;
	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public PlayerData playerData;
	public Timer gameOverTimer, freezeTimer;

	public bool GameIsPaused {get; private set;}
	public bool GameIsInterrupted {get; private set;}
	public bool GameIsOver {get; private set;}

	public void InitiatePlayerRespawn(PlayerRobotRespawn prr) => StartCoroutine(RespawnPlayer(prr));

	public void InterruptGame()
	{
		gameOverTimer.StartTimer();

		GameIsInterrupted = true;
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
		GameIsOver = true;

		DisablePlayer();
	}

	public void PauseGame()
	{
		if(GameIsInterrupted)
		{
			return;
		}
		
		GameIsPaused = !GameIsPaused;
		Time.timeScale = GameIsPaused ? 0f : 1f;

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

	private IEnumerator RespawnPlayer(PlayerRobotRespawn prr)
	{
		yield return new WaitForSeconds(playerRespawnDelay);

		prr.Respawn();

		if(playerData.Lives == 0)
		{
			gameOverTimer.duration = 0;
		}
	}
}