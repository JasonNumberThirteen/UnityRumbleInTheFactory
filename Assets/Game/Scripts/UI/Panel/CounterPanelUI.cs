using UnityEngine;

public class CounterPanelUI : MonoBehaviour
{
	protected IntCounter intCounter;

	private void Awake()
	{
		intCounter = GetComponentInChildren<IntCounter>();
	}
}