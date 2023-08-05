using UnityEngine;

public class RectTransformStretchTimedMover : MonoBehaviour
{
	public Vector2 targetOffsetMin, targetOffsetMax;
	public Timer timer;

	private RectTransform rectTransform;
	private Vector2 initialOffsetMin, initialOffsetMax;

	private void Awake() => rectTransform = GetComponent<RectTransform>();
	private bool ReachedTheTarget() => rectTransform.offsetMin == targetOffsetMin && rectTransform.offsetMax == targetOffsetMax;
	private float MinimumOffsetDifferenceX() => targetOffsetMin.x - initialOffsetMin.x;
	private float MinimumOffsetDifferenceY() => targetOffsetMin.y - initialOffsetMin.y;
	private float MaximumOffsetDifferenceX() => targetOffsetMax.x - initialOffsetMax.x;
	private float MaximumOffsetDifferenceY() => targetOffsetMax.y - initialOffsetMax.y;

	private void Start()
	{
		initialOffsetMin = rectTransform.offsetMin;
		initialOffsetMax = rectTransform.offsetMax;
	}

	private void Update()
	{
		if(timer.Started && !ReachedTheTarget())
		{
			SetOffset();
		}
	}
	
	private void SetOffset()
	{
		rectTransform.offsetMin = MinimumOffset();
		rectTransform.offsetMax = MaximumOffset();
	}
	
	private Vector2 MinimumOffset()
	{
		float percent = timer.ProgressPercent();
		float x = initialOffsetMin.x + MinimumOffsetDifferenceX()*percent;
		float y = initialOffsetMin.y + MinimumOffsetDifferenceY()*percent;

		return new Vector2(x, y);
	}

	private Vector2 MaximumOffset()
	{
		float percent = timer.ProgressPercent();
		float x = initialOffsetMax.x + MaximumOffsetDifferenceX()*percent;
		float y = initialOffsetMax.y + MaximumOffsetDifferenceY()*percent;

		return new Vector2(x, y);
	}
}