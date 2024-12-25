using UnityEngine;

public class IntCounter : MonoBehaviour
{
	public int initialValue;
	public CounterText text;
	public bool setAtStart = true;

	public int CurrentValue {get; private set;}

	protected virtual void SetValue(int value) => CurrentValue = value;
	protected virtual void IncreaseValue(int value) => CurrentValue += value;
	protected virtual void DecreaseValue(int value) => CurrentValue -= value;

	public void SetTo(int value)
	{
		SetValue(value);
		UpdateText();
	}

	public void IncreaseBy(int value)
	{
		IncreaseValue(value);
		UpdateText();
	}

	public void DecreaseBy(int value)
	{
		DecreaseValue(value);
		UpdateText();
	}

	protected virtual void Start()
	{
		if(setAtStart)
		{
			SetTo(initialValue);
		}
	}

	private void UpdateText()
	{
		if(text != null)
		{
			text.UpdateText();
		}
	}
}