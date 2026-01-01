using UnityEngine;

public class IntCounterPanelUI : MonoBehaviour
{
	private IntCounter intCounter;
	
	public void SetValueToCounter(int value)
	{
		if(intCounter != null)
		{
			intCounter.SetTo(value);
		}
	}

	protected virtual void Awake()
	{
		intCounter = GetComponentInChildren<IntCounter>();
	}
}