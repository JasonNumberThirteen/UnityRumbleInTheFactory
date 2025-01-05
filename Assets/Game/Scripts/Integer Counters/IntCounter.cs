using UnityEngine;
using UnityEngine.Events;

public class IntCounter : MonoBehaviour
{
	public UnityEvent valueChangedEvent;

	public int CurrentValue {get; private set;}

	public void SetTo(int value)
	{
		var previousValue = CurrentValue;
		
		CurrentValue = value;

		if(CurrentValue != previousValue)
		{
			valueChangedEvent?.Invoke();
		}
	}

	public virtual void IncreaseBy(int value)
	{
		SetTo(CurrentValue + value);
	}

	public virtual void DecreaseBy(int value)
	{
		SetTo(CurrentValue - value);
	}
}