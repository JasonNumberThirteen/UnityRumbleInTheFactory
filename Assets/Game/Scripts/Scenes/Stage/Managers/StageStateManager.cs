using UnityEngine;
using UnityEngine.Events;

public class StageStateManager : MonoBehaviour
{
	public UnityEvent<StageState> stageStateChangedEvent;
	
	private StageState stageState = StageState.ACTIVE;

	public bool StateIsSetTo(StageState stageState) => this.stageState == stageState;
	public bool GameIsOver() => StateIsSetTo(StageState.INTERRUPTED) || StateIsSetTo(StageState.OVER);

	public void SetStateTo(StageState stageState)
	{
		this.stageState = stageState;

		stageStateChangedEvent?.Invoke(stageState);
	}
}