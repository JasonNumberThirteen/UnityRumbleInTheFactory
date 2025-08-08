using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayersMovementStatesDictionary : Dictionary<PlayerRobotEntityMovementController, bool>
{
	public bool AnyPlayerIsMoving() => this.Any(pair => pair.Value);

	public void RegisterPlayerInput(bool register, PlayerRobotEntityMovementController playerRobotEntityMovementController, UnityAction<PlayerRobotEntityMovementController, bool> onMovementValueWasChanged, UnityAction<PlayerRobotEntityMovementController> onPlayerDied)
	{
		if(playerRobotEntityMovementController == null)
		{
			return;
		}
		
		if(register)
		{
			SetStateTo(playerRobotEntityMovementController, false);
			playerRobotEntityMovementController.movementValueWasChangedEvent.AddListener(onMovementValueWasChanged);
			playerRobotEntityMovementController.playerDiedEvent.AddListener(onPlayerDied);
		}
		else
		{
			Remove(playerRobotEntityMovementController);
			playerRobotEntityMovementController.movementValueWasChangedEvent.RemoveListener(onMovementValueWasChanged);
			playerRobotEntityMovementController.playerDiedEvent.RemoveListener(onPlayerDied);
		}
	}
	
	public void SetAllStates(bool isMoving)
	{
		Keys.ToList().ForEach(key => SetStateTo(key, isMoving));
	}

	public void SetStateTo(PlayerRobotEntityMovementController playerRobotEntityMovementController, bool isMoving)
	{
		if(playerRobotEntityMovementController != null)
		{
			this[playerRobotEntityMovementController] = isMoving;
		}
	}
}