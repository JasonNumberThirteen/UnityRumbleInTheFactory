using UnityEngine;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
	public UnityEvent onSelect, onSubmit;

	public void Select()
	{
		onSelect?.Invoke();
	}

	public void Submit()
	{
		onSubmit?.Invoke();
	}
}