using UnityEngine;

public class Counter : MonoBehaviour
{
	public int initialValue;

	public int CurrentValue {get; private set;}

	public void SetTo(int value) => CurrentValue = value;
	public void IncreaseBy(int value) => CurrentValue += value;
	public void DecreaseBy(int value) => CurrentValue -= value;

	private void Start() => SetTo(initialValue);
}