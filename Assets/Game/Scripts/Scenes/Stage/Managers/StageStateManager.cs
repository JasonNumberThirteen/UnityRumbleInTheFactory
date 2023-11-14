using UnityEngine;

public class StageStateManager : MonoBehaviour
{
	private GameStates state = GameStates.ACTIVE;

	private enum GameStates
	{
		ACTIVE, PAUSED, INTERRUPTED, WON, OVER
	}

	public void SwitchPauseState()
	{
		if(IsActive())
		{
			SetAsPaused();
		}
		else
		{
			SetAsActive();
		}
	}

	public bool GameIsOver() => IsInterrupted() || IsOver();
	public bool IsActive() => state == GameStates.ACTIVE;
	public bool IsPaused() => state == GameStates.PAUSED;
	public bool IsInterrupted() => state == GameStates.INTERRUPTED;
	public bool IsWon() => state == GameStates.WON;
	public bool IsOver() => state == GameStates.OVER;
	public void SetAsActive() => state = GameStates.ACTIVE;
	public void SetAsPaused() => state = GameStates.PAUSED;
	public void SetAsInterrupted() => state = GameStates.INTERRUPTED;
	public void SetAsWon() => state = GameStates.WON;
	public void SetAsOver() => state = GameStates.OVER;
}