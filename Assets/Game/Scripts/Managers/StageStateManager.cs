using UnityEngine;
using UnityEngine.Events;

public class StageStateManager : MonoBehaviour
{
	public UnityEvent<StageState> stageStateChangedEvent;
	
	private StageState stageState = StageState.Active;

	public bool StateIsSetTo(StageState stageState) => this.stageState == stageState;
	public bool GameIsOver() => StateIsSetTo(StageState.Interrupted) || StateIsSetTo(StageState.Over);
	public StageState GetCurrentState() => stageState;

	public void SetStateTo(StageState stageState)
	{
		this.stageState = stageState;

		stageStateChangedEvent?.Invoke(stageState);
	}
}