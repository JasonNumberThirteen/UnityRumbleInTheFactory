using UnityEngine;

public class PlayerRobotMovementSoundManager : MonoBehaviour
{
	private PlayerRobotEntitySpawner[] playerRobotEntitySpawners;
	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;

	private readonly PlayersMovementStatesDictionary playersMovementStates = new();

	private void Awake()
	{
		playerRobotEntitySpawners = ObjectMethods.FindComponentsOfType<PlayerRobotEntitySpawner>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();
		stageStateManager = ObjectMethods.FindComponentOfType<StageStateManager>();

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
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.AddListener(OnStageStateChanged);
			}
		}
		else
		{
			if(stageStateManager != null)
			{
				stageStateManager.stageStateChangedEvent.RemoveListener(OnStageStateChanged);
			}
		}

		RegisterToPlayerRobotEntitySpawnerListeners(register);
	}

	private void OnStageStateChanged(StageState stageState)
	{
		if(stageState != StageState.Over)
		{
			return;
		}

		playersMovementStates.SetAllStates(false);
		UpdateMovementSound();
	}

	private void RegisterToPlayerRobotEntitySpawnerListeners(bool register)
	{
		playerRobotEntitySpawners.ForEach(playerRobotEntitySpawner =>
		{
			if(register)
			{
				playerRobotEntitySpawner.entitySpawnedEvent.AddListener(OnEntitySpawned);
			}
			else
			{
				playerRobotEntitySpawner.entitySpawnedEvent.RemoveListener(OnEntitySpawned);
			}
		});
	}

	private void OnEntitySpawned(GameObject go)
	{
		if(go.TryGetComponent(out PlayerRobotEntityMovementController playerRobotEntityMovementController))
		{
			RegisterPlayerRobotEntityInput(playerRobotEntityMovementController, true);
		}
	}

	private void OnPlayerDied(PlayerRobotEntityMovementController playerRobotEntityMovementController)
	{
		RegisterPlayerRobotEntityInput(playerRobotEntityMovementController, false);
	}

	private void RegisterPlayerRobotEntityInput(PlayerRobotEntityMovementController playerRobotEntityMovementController, bool register)
	{
		playersMovementStates.RegisterPlayerInput(register, playerRobotEntityMovementController, OnMovementValueChanged, OnPlayerDied);
		UpdateMovementSound();
	}

	private void OnMovementValueChanged(PlayerRobotEntityMovementController playerRobotEntityMovementController, bool isMoving)
	{
		playersMovementStates.SetStateTo(playerRobotEntityMovementController, isMoving);
		UpdateMovementSound();
	}

	private void UpdateMovementSound()
	{
		if(stageSoundManager == null)
		{
			return;
		}
		
		var soundEffectType = playersMovementStates.AnyPlayerIsMoving() ? SoundEffectType.PlayerRobotMovement : SoundEffectType.PlayerRobotIdle;
		
		stageSoundManager.PlaySound(soundEffectType);
	}
}