using UnityEngine;

public class CounterPanelUI : MonoBehaviour
{
	protected IntCounter intCounter;

	protected virtual void Awake()
	{
		intCounter = GetComponentInChildren<IntCounter>();
	}
}