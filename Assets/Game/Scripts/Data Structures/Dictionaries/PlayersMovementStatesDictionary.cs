using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlayersMovementStatesDictionary : Dictionary<PlayerRobotEntityInput, bool>
{
	public bool AnyPlayerIsMoving() => this.Any(pair => pair.Value);

	public void RegisterPlayerInput(bool register, PlayerRobotEntityInput playerRobotEntityInput, UnityAction<PlayerRobotEntityInput, bool> onMovementValueChanged, UnityAction<PlayerRobotEntityInput> onPlayerDied)
	{
		if(playerRobotEntityInput == null)
		{
			return;
		}
		
		if(register)
		{
			SetStateTo(playerRobotEntityInput, false);
			playerRobotEntityInput.movementValueChangedEvent.AddListener(onMovementValueChanged);
			playerRobotEntityInput.playerDiedEvent.AddListener(onPlayerDied);
		}
		else
		{
			Remove(playerRobotEntityInput);
			playerRobotEntityInput.movementValueChangedEvent.RemoveListener(onMovementValueChanged);
			playerRobotEntityInput.playerDiedEvent.RemoveListener(onPlayerDied);
		}
	}
	
	public void SetAllStates(bool isMoving)
	{
		Keys.ToList().ForEach(key => SetStateTo(key, isMoving));
	}

	public void SetStateTo(PlayerRobotEntityInput playerRobotEntityInput, bool isMoving)
	{
		if(playerRobotEntityInput != null)
		{
			this[playerRobotEntityInput] = isMoving;
		}
	}
}