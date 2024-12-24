using UnityEngine;

public class StageStateManager : MonoBehaviour
{
	private GameState state = GameState.ACTIVE;

	public void SwitchPauseState()
	{
		if(StateIsSetTo(GameState.ACTIVE))
		{
			SetStateTo(GameState.PAUSED);
		}
		else
		{
			SetStateTo(GameState.ACTIVE);
		}
	}

	public bool StateIsSetTo(GameState gameState) => state == gameState;
	public bool GameIsOver() => StateIsSetTo(GameState.INTERRUPTED) || StateIsSetTo(GameState.OVER);

	public void SetStateTo(GameState gameState)
	{
		state = gameState;
	}
}