using UnityEngine;

public class StageStateManager : MonoBehaviour
{
	private GameState state = GameState.ACTIVE;

	public bool StateIsSetTo(GameState gameState) => state == gameState;
	public bool GameIsOver() => StateIsSetTo(GameState.INTERRUPTED) || StateIsSetTo(GameState.OVER);

	public void SetStateTo(GameState gameState)
	{
		state = gameState;
	}
}