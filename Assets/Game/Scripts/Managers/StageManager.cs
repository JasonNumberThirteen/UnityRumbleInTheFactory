using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
	public static StageManager instance = null;

	[SerializeField] private GameData gameData;

	private StageStateManager stageStateManager;
	private EnemyRobotEntitySpawnManager enemyRobotEntitySpawnManager;
	private NukeEntity nukeEntity;
	private int numberOfDefeatedEnemies;

	public void CountDefeatedEnemy()
	{
		++numberOfDefeatedEnemies;

		if(stageStateManager != null && WonStage())
		{
			stageStateManager.SetStateTo(StageState.Won);
		}
	}

	public void PauseGameIfPossible()
	{
		var stageStatesBlockingPause = new List<StageState>
		{
			StageState.Interrupted,
			StageState.Won,
			StageState.Over
		};
		
		if(stageStateManager == null || stageStatesBlockingPause.Contains(stageStateManager.GetStageState()))
		{
			return;
		}

		var stateToSwitch = stageStateManager.StateIsSetTo(StageState.Active) ? StageState.Paused : StageState.Active;

		stageStateManager.SetStateTo(stateToSwitch);
	}

	public void SetGameAsOver()
	{
		gameData.SetGameAsOver();
		stageStateManager.SetStateTo(StageState.Over);
	}
	
	private void Awake()
	{
		CheckInstanceReference();
		
		stageStateManager = FindAnyObjectByType<StageStateManager>(FindObjectsInactive.Include);
		enemyRobotEntitySpawnManager = FindAnyObjectByType<EnemyRobotEntitySpawnManager>(FindObjectsInactive.Include);
		nukeEntity = FindAnyObjectByType<NukeEntity>(FindObjectsInactive.Include);

		RegisterToListeners(true);
	}

	private void CheckInstanceReference()
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

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			nukeEntity.nukeDestroyedEvent.AddListener(OnNukeDestroyed);
		}
		else
		{
			nukeEntity.nukeDestroyedEvent.RemoveListener(OnNukeDestroyed);
		}
	}

	private void OnNukeDestroyed()
	{
		if(stageStateManager != null)
		{
			stageStateManager.SetStateTo(StageState.Interrupted);
		}
	}

	private bool WonStage()
	{
		var enemyRobotEntitySpawnManagerExists = enemyRobotEntitySpawnManager != null;
		var totalNumberOfEnemies = enemyRobotEntitySpawnManagerExists ? enemyRobotEntitySpawnManager.GetTotalNumberOfEnemies() : 0;
		var noEnemiesLeft = !enemyRobotEntitySpawnManagerExists || enemyRobotEntitySpawnManager.NoEnemiesLeft();
		
		return numberOfDefeatedEnemies >= totalNumberOfEnemies && noEnemiesLeft;
	}
}