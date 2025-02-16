using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayersMovementStatesDictionary : Dictionary<PlayerRobotEntityInputController, bool>
{
	public bool AnyPlayerIsMoving() => this.Any(pair => pair.Value);

	public void RegisterPlayerInput(bool register, PlayerRobotEntityInputController playerRobotEntityInputController, UnityAction<PlayerRobotEntityInputController, bool> onMovementValueChanged, UnityAction<PlayerRobotEntityInputController> onPlayerDied)
	{
		if(playerRobotEntityInputController == null)
		{
			return;
		}
		
		if(register)
		{
			SetStateTo(playerRobotEntityInputController, false);
			playerRobotEntityInputController.movementValueChangedEvent.AddListener(onMovementValueChanged);
			playerRobotEntityInputController.playerDiedEvent.AddListener(onPlayerDied);
		}
		else
		{
			Remove(playerRobotEntityInputController);
			playerRobotEntityInputController.movementValueChangedEvent.RemoveListener(onMovementValueChanged);
			playerRobotEntityInputController.playerDiedEvent.RemoveListener(onPlayerDied);
		}
	}
	
	public void SetAllStates(bool isMoving)
	{
		Keys.ToList().ForEach(key => SetStateTo(key, isMoving));
	}

	public void SetStateTo(PlayerRobotEntityInputController playerRobotEntityInputController, bool isMoving)
	{
		if(playerRobotEntityInputController != null)
		{
			this[playerRobotEntityInputController] = isMoving;
		}
	}
}