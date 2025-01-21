using UnityEngine;
using UnityEngine.Events;

public class IntCounter : MonoBehaviour
{
	public UnityEvent valueChangedEvent;

	public int CurrentValue {get; private set;}

	public void SetTo(int value)
	{
		var previousValue = CurrentValue;
		
		CurrentValue = GetModifiedValue(value);

		if(CurrentValue != previousValue)
		{
			valueChangedEvent?.Invoke();
		}
	}

	public virtual void ModifyBy(int value)
	{
		SetTo(CurrentValue + value);
	}

	protected virtual int GetModifiedValue(int value) => value;
}