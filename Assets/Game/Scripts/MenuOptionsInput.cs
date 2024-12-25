using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class MenuOptionsInput : MonoBehaviour
{
	public UnityEvent<int> navigateKeyPressedEvent;
	public UnityEvent submitKeyPressedEvent;
	public UnityEvent cancelKeyPressedEvent;
	
	[SerializeField] private Axis movementAxis;
	
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
		return movementAxis switch
		{
			Axis.HORIZONTAL => Mathf.RoundToInt(inputVector.x),
			Axis.VERTICAL => Mathf.RoundToInt(-inputVector.y),
			_ => 0
		};
	}
}