using UnityEngine;

[RequireComponent(typeof(RobotEntityRankController))]
public class RobotEntityAnimatorController : EntityAnimatorController
{
	[SerializeField] private VerticalDirection initialVerticalDirection = VerticalDirection.Up;

	private float movementSpeed;
	private RobotEntityRankController robotEntityRankController;
	
	private readonly string MOVEMENT_SPEED_PARAMETER_NAME = "MovementSpeed";

	protected override void Awake()
	{
		base.Awake();
		
		robotEntityRankController = GetComponent<RobotEntityRankController>();

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
			robotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			robotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	private void OnRankChanged(RobotRank robotRank)
	{
		if(robotRank != null)
		{
			animator.runtimeAnimatorController = robotRank.GetRuntimeAnimatorController();
		}
	}

	private void Start()
	{
		animator.SetInteger(VERTICAL_MOVEMENT_PARAMETER_NAME, GetInitialVerticalDirection());
	}

	private int GetInitialVerticalDirection()
	{
		return initialVerticalDirection switch
		{
			VerticalDirection.Up => 1,
			VerticalDirection.Down => -1,
			_ => 0
		};
	}
	
	private void Update()
	{
		var currentMovementDirectionIsNone = entityMovementController.CurrentMovementDirectionIsNone();
		var currentMovementSpeed = currentMovementDirectionIsNone ? 0f : 1f;
		
		if(!Mathf.Approximately(movementSpeed, currentMovementSpeed))
		{
			movementSpeed = currentMovementSpeed;
			
			animator.SetFloat(MOVEMENT_SPEED_PARAMETER_NAME, movementSpeed);
		}

		if(!currentMovementDirectionIsNone)
		{
			UpdateMovementParametersValues();
		}
	}
}