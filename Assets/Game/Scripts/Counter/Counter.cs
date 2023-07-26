using UnityEngine;

public class Counter : MonoBehaviour
{
	public int initialValue;
	public CounterText text;
	public bool setAtStart = true;

	public int CurrentValue {get; private set;}

	public void SetTo(int value)
	{
		CurrentValue = value;

		UpdateText();
	}

	public void IncreaseBy(int value)
	{
		CurrentValue += value;

		UpdateText();
	}

	public void DecreaseBy(int value)
	{
		CurrentValue -= value;

		UpdateText();
	}

	private void Start()
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