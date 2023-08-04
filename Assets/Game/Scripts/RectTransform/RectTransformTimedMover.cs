using UnityEngine;

public class RectTransformTimedMover : RectTransformMover
{
	public Vector2 targetPosition;
	public Timer timer;

	private Vector2 initialPosition;

	protected override void Awake()
	{
		base.Awake();
		
		initialPosition = rectTransform.anchoredPosition;
	}

	private void Update()
	{
		if(!ReachedTheTarget())
		{
			SetPosition();
		}
	}

	private bool ReachedTheTarget() => rectTransform.anchoredPosition == targetPosition;
	private void SetPosition() => rectTransform.anchoredPosition = CurrentPosition();
	private float DifferenceX() => targetPosition.x - initialPosition.x;
	private float DifferenceY() => targetPosition.y - initialPosition.y;

	private Vector2 CurrentPosition()
	{
		float percent = timer.ProgressPercent();
		float x = initialPosition.x + DifferenceX()*percent;
		float y = initialPosition.y + DifferenceY()*percent;

		return new Vector2(x, y);
	}
}