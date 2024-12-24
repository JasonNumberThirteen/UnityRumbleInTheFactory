using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	public StageUIManager uiManager;
	public StageStateManager stateManager;
	public StageAudioManager audioManager;
	public PlayersManager playersManager;
	public EnemySpawnManager enemySpawnManager;
	public EnemyFreezeManager enemyFreezeManager;
	public GameData gameData;
	public Timer gameOverTimer, sceneManagerTimer;

	private int defeatedEnemies;

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

	public void PauseGame()
	{
		if(stateManager.StateIsSetTo(GameState.INTERRUPTED) || stateManager.StateIsSetTo(GameState.WON) || stateManager.StateIsSetTo(GameState.OVER))
		{
			return;
		}

		stateManager.SetStateTo(stateManager.StateIsSetTo(GameState.ACTIVE) ? GameState.PAUSED : GameState.ACTIVE);
		uiManager.ControlPauseTextDisplay();
		audioManager.SwitchPlayerRobotMovementChannel();

		Time.timeScale = stateManager.StateIsSetTo(GameState.PAUSED) ? 0f : 1f;
	}

	public void InterruptGame()
	{
		stateManager.SetStateTo(GameState.INTERRUPTED);
		gameOverTimer.StartTimer();
	}

	public void SetGameAsOver()
	{
		gameData.isOver = true;

		stateManager.SetStateTo(GameState.OVER);
		playersManager.DisablePlayers();
		audioManager.StopPlayerRobotMovementChannel();
	}

	public void EnableAudioManager()
	{
		if(stateManager.StateIsSetTo(GameState.OVER))
		{
			return;
		}

		audioManager.gameObject.SetActive(true);
	}
	
	private void Awake()
	{
		CheckSingleton();
		playersManager.FindPlayers();
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

	private void CheckIfWonTheGame()
	{
		if(!WonTheGame())
		{
			return;
		}

		stateManager.SetStateTo(GameState.WON);
		sceneManagerTimer.StartTimer();
	}
}