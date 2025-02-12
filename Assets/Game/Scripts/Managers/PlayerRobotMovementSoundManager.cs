using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobotMovementSoundManager : MonoBehaviour
{
	private PlayerRobotEntitySpawner[] playerRobotEntitySpawners;
	private StageSoundManager stageSoundManager;
	private StageStateManager stageStateManager;

	private readonly Dictionary<PlayerRobotEntityInput, bool> movementValueChangedByPlayerRobotEntityInput = new();

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

		movementValueChangedByPlayerRobotEntityInput.Keys.ToList().ForEach(key => movementValueChangedByPlayerRobotEntityInput[key] = false);
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
		if(go.TryGetComponent(out PlayerRobotEntityInput playerRobotEntityInput))
		{
			RegisterPlayerRobotEntityInput(playerRobotEntityInput, true);
		}
	}

	private void OnMovementValueChanged(PlayerRobotEntityInput playerRobotEntityInput, bool isMoving)
	{
		movementValueChangedByPlayerRobotEntityInput[playerRobotEntityInput] = isMoving;

		UpdateMovementSound();
	}

	private void OnPlayerDied(PlayerRobotEntityInput playerRobotEntityInput)
	{
		RegisterPlayerRobotEntityInput(playerRobotEntityInput, false);
	}

	private void RegisterPlayerRobotEntityInput(PlayerRobotEntityInput playerRobotEntityInput, bool register)
	{
		if(register)
		{
			movementValueChangedByPlayerRobotEntityInput.Add(playerRobotEntityInput, false);
			playerRobotEntityInput.movementValueChangedEvent.AddListener(OnMovementValueChanged);
			playerRobotEntityInput.playerDiedEvent.AddListener(OnPlayerDied);
		}
		else
		{
			movementValueChangedByPlayerRobotEntityInput.Remove(playerRobotEntityInput);
			playerRobotEntityInput.movementValueChangedEvent.RemoveListener(OnMovementValueChanged);
			playerRobotEntityInput.playerDiedEvent.RemoveListener(OnPlayerDied);
		}

		UpdateMovementSound();
	}

	private void UpdateMovementSound()
	{
		if(stageSoundManager == null)
		{
			return;
		}
		
		var soundEffectType = movementValueChangedByPlayerRobotEntityInput.Any(pair => pair.Value) ? SoundEffectType.PlayerRobotMovement : SoundEffectType.PlayerRobotIdle;
		
		stageSoundManager.PlaySound(soundEffectType);
	}
}