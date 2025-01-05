using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	public StageUIManager uiManager;
	public EnemySpawnManager enemySpawnManager;
	public GameData gameData;

	[SerializeField] private PlayersListData playersListData;

	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;
	private Nuke nuke;
	private int defeatedEnemies;

	public void CountDefeatedEnemy()
	{
		++defeatedEnemies;

		CheckIfWonTheGame();
	}

	public void PauseGame()
	{
		if(stageStateManager.StateIsSetTo(StageState.Interrupted) || stageStateManager.StateIsSetTo(StageState.Won) || stageStateManager.StateIsSetTo(StageState.Over))
		{
			return;
		}

		stageStateManager.SetStateTo(stageStateManager.StateIsSetTo(StageState.Active) ? StageState.Paused : StageState.Active);
		uiManager.ControlPauseTextDisplay();

		Time.timeScale = stageStateManager.StateIsSetTo(StageState.Paused) ? 0f : 1f;
	}

	public void SetGameAsOver()
	{
		gameData.SetGameAsOver();
		stageStateManager.SetStateTo(StageState.Over);
	}

	public void CheckPlayersLives()
	{
		if(playersListData != null && !playersListData.Any(playerData => playerData.Spawner != null && playerData.Lives > 0))
		{
			SetGameAsOver();
		}
	}

	public void EnableAudioManager()
	{
		if(stageSoundManager == null || stageStateManager.StateIsSetTo(StageState.Over))
		{
			return;
		}

		stageSoundManager.gameObject.SetActive(true);
	}
	
	private void Awake()
	{
		CheckSingleton();
		
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
		stageStateManager = FindAnyObjectByType<StageStateManager>(FindObjectsInactive.Include);
		nuke = FindAnyObjectByType<Nuke>(FindObjectsInactive.Include);

		if(playersListData != null)
		{
			playersListData.ForEach(playerData => playerData.ResetDefeatedEnemies());
		}

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
		stageStateManager.SetStateTo(StageState.Interrupted);
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
			stageStateManager.SetStateTo(StageState.Won);
		}
	}
}