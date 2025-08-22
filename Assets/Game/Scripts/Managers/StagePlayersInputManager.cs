using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StagePlayersInputManager : MonoBehaviour
{
	[SerializeField] private InputActionAsset inputActions;
	[SerializeField] private GameData gameData;
	
	private List<PlayerRobotEntitySpawner> activePlayerRobotEntitySpawners = new();
	private StageSceneFlowManager stageSceneFlowManager;

	public InputActionAsset GetInputActions() => inputActions;
	public GameData GetGameData() => gameData;

	private void Awake()
	{
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();

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
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageWasActivatedEvent.AddListener(OnStageWasActivated);
			}
		}
		else
		{
			if(stageSceneFlowManager != null)
			{
				stageSceneFlowManager.stageWasActivatedEvent.RemoveListener(OnStageWasActivated);
			}
		}
	}

	private void OnStageWasActivated()
	{
		activePlayerRobotEntitySpawners = ObjectMethods.FindComponentsOfType<PlayerRobotEntitySpawner>(false).OrderBy(GetPlayerOrdinalNumberFromSpawner).ToList();

		activePlayerRobotEntitySpawners?.ForEachIndexed(CreatePlayerInputControllerInstance, 1);
	}

	private void CreatePlayerInputControllerInstance(PlayerRobotEntitySpawner playerRobotEntitySpawner, int counterValue)
	{
		var playerInputControllerGO = new GameObject
		{
			name = GetInputControllerName(counterValue)
		};

		playerInputControllerGO.transform.SetParent(transform);
		playerInputControllerGO.AddComponent<PlayerInputController>().Setup(playerRobotEntitySpawner);
	}

	private int GetPlayerOrdinalNumberFromSpawner(PlayerRobotEntitySpawner playerRobotEntitySpawner) => RobotDataMethods.GetOrdinalNumber(playerRobotEntitySpawner.GetPlayerRobotData());
	private string GetInputControllerName(int ordinalNumber) => $"Player {ordinalNumber} Input Controller";
}