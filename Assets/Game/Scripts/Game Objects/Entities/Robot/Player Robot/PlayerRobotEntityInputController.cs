using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerRobotEntity))]
public class PlayerRobotEntityInputController : MonoBehaviour
{
	public UnityEvent<Vector2> movementKeyWasPressedEvent;
	public UnityEvent shootKeyWasPressedEvent;
	
	[SerializeField] private GameData gameData;
	
	private PlayerInput playerInput;
	private PlayerRobotEntity playerRobotEntity;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		playerRobotEntity = GetComponent<PlayerRobotEntity>();
		stageSceneFlowManager = ObjectMethods.FindComponentOfType<StageSceneFlowManager>();
	}

	private void Start()
	{
		SetControlScheme();
	}

	private void SetControlScheme()
	{
		var controlSchemeName = ShouldAssignGamepadControlScheme() ? InputMethods.GAMEPAD_CONTROL_SCHEME_NAME : InputMethods.KEYBOARD_AND_MOUSE_CONTROL_SCHEME_NAME;
	
		InputMethods.SetControlSchemeTo(playerInput, controlSchemeName);
	}

	private bool ShouldAssignGamepadControlScheme()
	{
		var selectedTwoPlayerMode = GameDataMethods.SelectedTwoPlayerMode(gameData);
		var isFirstPlayer = PlayerHasOrdinalNumber(1);
		
		return Gamepad.current != null && (!selectedTwoPlayerMode || !isFirstPlayer);
	}

	private bool PlayerHasOrdinalNumber(int ordinalNumber)
	{
		var playerRobotData = playerRobotEntity.GetPlayerRobotData();

		return playerRobotData != null && playerRobotData.GetOrdinalNumber() == ordinalNumber;
	}

	private void OnMove(InputValue inputValue)
	{
		movementKeyWasPressedEvent?.Invoke(inputValue.Get<Vector2>());
	}

	private void OnFire(InputValue inputValue)
	{
		shootKeyWasPressedEvent?.Invoke();
	}

	private void OnPause(InputValue inputValue)
	{
		if(stageSceneFlowManager != null)
		{
			stageSceneFlowManager.PauseGameIfPossible();
		}
	}
}