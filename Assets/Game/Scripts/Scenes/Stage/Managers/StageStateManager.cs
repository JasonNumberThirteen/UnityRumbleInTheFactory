using UnityEngine;

public class StageStateManager : MonoBehaviour
{
	private GameState state = GameState.ACTIVE;

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
	public bool IsActive() => state == GameState.ACTIVE;
	public bool IsPaused() => state == GameState.PAUSED;
	public bool IsInterrupted() => state == GameState.INTERRUPTED;
	public bool IsWon() => state == GameState.WON;
	public bool IsOver() => state == GameState.OVER;
	public void SetAsActive() => state = GameState.ACTIVE;
	public void SetAsPaused() => state = GameState.PAUSED;
	public void SetAsInterrupted() => state = GameState.INTERRUPTED;
	public void SetAsWon() => state = GameState.WON;
	public void SetAsOver() => state = GameState.OVER;
}