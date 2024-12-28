using UnityEngine;

[RequireComponent(typeof(Timer))]
public class TimedRectTransformPositionController : RectTransformPositionController
{
	[SerializeField] private Vector2 targetPosition;
	
	private Timer timer;
	private Vector2 initialPosition;

	protected override void Awake()
	{
		base.Awake();
		
		timer = GetComponent<Timer>();
		initialPosition = rectTransform.anchoredPosition;
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