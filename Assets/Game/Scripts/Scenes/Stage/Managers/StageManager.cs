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
	public GameData gameData;

	private Nuke nuke;
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
		if(stateManager.StateIsSetTo(StageState.Interrupted) || stateManager.StateIsSetTo(StageState.Won) || stateManager.StateIsSetTo(StageState.Over))
		{
			return;
		}

		stateManager.SetStateTo(stateManager.StateIsSetTo(StageState.Active) ? StageState.Paused : StageState.Active);
		uiManager.ControlPauseTextDisplay();
		audioManager.SwitchPlayerRobotMovementChannel();

		Time.timeScale = stateManager.StateIsSetTo(StageState.Paused) ? 0f : 1f;
	}

	public void SetGameAsOver()
	{
		gameData.SetGameAsOver();
		stateManager.SetStateTo(StageState.Over);
		playersManager.DisablePlayers();
		audioManager.StopPlayerRobotMovementChannel();
	}

	public void EnableAudioManager()
	{
		if(stateManager.StateIsSetTo(StageState.Over))
		{
			return;
		}

		audioManager.gameObject.SetActive(true);
	}
	
	private void Awake()
	{
		CheckSingleton();
		playersManager.FindPlayers();

		nuke = FindAnyObjectByType<Nuke>(FindObjectsInactive.Include);

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			nuke.nukeDestroyedEvent.AddListener(OnNukeDestroyed);
		}
		else
		{
			nuke.nukeDestroyedEvent.RemoveListener(OnNukeDestroyed);
		}
	}

	private void OnNukeDestroyed()
	{
		stateManager.SetStateTo(StageState.Interrupted);
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
		if(WonTheGame())
		{
			stateManager.SetStateTo(StageState.Won);
		}
	}
}