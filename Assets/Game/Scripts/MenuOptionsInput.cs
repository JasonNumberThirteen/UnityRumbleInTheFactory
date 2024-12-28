using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MenuOptionsInput : MonoBehaviour
{
	public UnityEvent<int> navigateKeyPressedEvent;
	public UnityEvent submitKeyPressedEvent;
	public UnityEvent cancelKeyPressedEvent;
	
	[SerializeField] private Axis navigationAxis;

	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}
	
	private void OnNavigate(InputValue inputValue)
	{
		var inputVector = inputValue.Get<Vector2>();

		navigateKeyPressedEvent?.Invoke(GetNavigationValue(inputVector));
	}

	private void OnSubmit(InputValue inputValue)
	{
		submitKeyPressedEvent?.Invoke();
	}

	private void OnCancel(InputValue inputValue)
	{
		cancelKeyPressedEvent?.Invoke();
	}

	private int GetNavigationValue(Vector2 inputVector)
	{
		return navigationAxis switch
		{
			Axis.Horizontal => Mathf.RoundToInt(inputVector.x),
			Axis.Vertical => Mathf.RoundToInt(-inputVector.y),
			_ => 0
		};
	}
}