using UnityEngine;
using UnityEngine.Events;

public class IntCounter : MonoBehaviour
{
	public int initialValue;
	public IntCounterTextUI text;
	public bool setAtStart = true;
	public UnityEvent valueChangedEvent;

	public int CurrentValue {get; private set;}

	protected virtual void SetValue(int value)
	{
		CurrentValue = value;

		valueChangedEvent?.Invoke();
	}

	protected virtual void IncreaseValue(int value)
	{
		CurrentValue += value;

		valueChangedEvent?.Invoke();
	}

	protected virtual void DecreaseValue(int value)
	{
		CurrentValue -= value;

		valueChangedEvent?.Invoke();
	}

	public void SetTo(int value)
	{
		SetValue(value);
	}

	public void IncreaseBy(int value)
	{
		IncreaseValue(value);
	}

	public void DecreaseBy(int value)
	{
		DecreaseValue(value);
	}

	protected virtual void Start()
	{
		if(setAtStart)
		{
			SetTo(initialValue);
		}
	}
}