using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class TimedRectTransformPositionController : RectTransformPositionController
{
	public UnityEvent rectTransformReachedTargetPositionEvent;
	
	[SerializeField] private Vector2 targetPosition;
	
	private Timer timer;
	private Vector2 initialPosition;

	public void SetInitialPosition(Vector2 initialPosition)
	{
		this.initialPosition = initialPosition;
	}

	public void SetTargetPosition(Vector2 targetPosition)
	{
		this.targetPosition = targetPosition;
	}

	public void StartTranslation()
	{
		timer.StartTimer();
	}

	protected override void Awake()
	{
		base.Awake();
		
		timer = GetComponent<Timer>();
		initialPosition = rectTransform.anchoredPosition;

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerEnd()
	{
		rectTransformReachedTargetPositionEvent?.Invoke();
	}

	private void Update()
	{
		if(timer.Started && !ReachedTargetPosition())
		{
			rectTransform.anchoredPosition = GetCurrentPosition();
		}
	}

	private Vector2 GetCurrentPosition()
	{
		var percent = timer.ProgressPercent();
		var x = initialPosition.x + GetDifferenceX()*percent;
		var y = initialPosition.y + GetDifferenceY()*percent;

		return new Vector2(x, y);
	}

	private bool ReachedTargetPosition() => rectTransform.anchoredPosition == targetPosition;
	private float GetDifferenceX() => targetPosition.x - initialPosition.x;
	private float GetDifferenceY() => targetPosition.y - initialPosition.y;
}