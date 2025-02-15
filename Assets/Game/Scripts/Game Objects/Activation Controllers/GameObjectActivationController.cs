using UnityEngine;

[DefaultExecutionOrder(-200)]
public abstract class GameObjectActivationController : MonoBehaviour
{
	protected abstract bool GOShouldBeActive();

	protected virtual bool GOActivationStateCanBeChanged() => true;
	
	private void Awake()
	{
		if(GOActivationStateCanBeChanged())
		{
			gameObject.SetActive(GOShouldBeActive());	
		}
	}
}