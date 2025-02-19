using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput), typeof(PlayerRobotEntityMovementController))]
public class PlayerRobotEntityInputController : MonoBehaviour
{
	public UnityEvent<Vector2> movementValueChangedEvent;
	public UnityEvent shootKeyPressedEvent;
	public UnityEvent componentActivatedEvent;
	public UnityEvent componentDeactivatedEvent;
	
	[SerializeField] private GameData gameData;
	[SerializeField, Min(1)] private int ordinalNumber;
	
	private PlayerInput playerInput;
	private StageSceneFlowManager stageSceneFlowManager;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
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
		var selectedTwoPlayersMode = GameDataMethods.SelectedTwoPlayersMode(gameData);
		var isFirstPlayer = ordinalNumber == 1;

		return Gamepad.current != null && (!selectedTwoPlayersMode || !isFirstPlayer);
	}

	private void OnMove(InputValue inputValue)
	{
		if(enabled)
		{
			movementValueChangedEvent?.Invoke(inputValue.Get<Vector2>());
		}
	}

	private void OnFire(InputValue inputValue)
	{
		if(enabled)
		{
			shootKeyPressedEvent?.Invoke();
		}
	}

	private void OnPause(InputValue inputValue)
	{
		if(stageSceneFlowManager != null)
		{
			stageSceneFlowManager.PauseGameIfPossible();
		}
	}

	private void OnEnable()
	{
		componentActivatedEvent?.Invoke();
	}

	private void OnDisable()
	{
		componentDeactivatedEvent?.Invoke();
	}
}