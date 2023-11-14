using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[Min(0)] public int pointsForBonus = 500;
	public StageUIManager uiManager;
	public StageStateManager stateManager;
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
		playersManager.DisablePlayers();
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

		stateManager.SetAsWon();
		sceneManagerTimer.StartTimer();
	}
}