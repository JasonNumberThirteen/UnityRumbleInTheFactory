using UnityEngine;

public class StageStateManager : MonoBehaviour
{
	private GameState gameState = GameState.ACTIVE;

	public bool StateIsSetTo(GameState gameState) => this.gameState == gameState;
	public bool GameIsOver() => StateIsSetTo(GameState.INTERRUPTED) || StateIsSetTo(GameState.OVER);

	public void SetStateTo(GameState gameState)
	{
		this.gameState = gameState;
	}
}