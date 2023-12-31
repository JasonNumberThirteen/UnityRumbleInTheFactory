using UnityEngine;

public class RectTransformStretchTimedMover : MonoBehaviour
{
	public enum WidthSide
	{
		NONE, LEFT, RIGHT
	}

	public enum HeightSide
	{
		NONE, TOP, BOTTOM
	}
	
	public Timer timer;
	public WidthSide widthSide;
	public HeightSide heightSide;
	public bool reverseDirection = false;

	private RectTransform rectTransform;
	private Vector2 initialOffsetMin, initialOffsetMax, targetOffsetMin, targetOffsetMax;

	private void Awake() => rectTransform = GetComponent<RectTransform>();
	private void Start() => SetValues();
	private bool ReachedTheTarget() => rectTransform.offsetMin == targetOffsetMin && rectTransform.offsetMax == targetOffsetMax;
	private float MinimumOffsetDifferenceX() => targetOffsetMin.x - initialOffsetMin.x;
	private float MinimumOffsetDifferenceY() => targetOffsetMin.y - initialOffsetMin.y;
	private float MaximumOffsetDifferenceX() => targetOffsetMax.x - initialOffsetMax.x;
	private float MaximumOffsetDifferenceY() => targetOffsetMax.y - initialOffsetMax.y;

	private void SetValues()
	{
		rectTransform.offsetMin = InitialMinimumOffset();
		rectTransform.offsetMax = InitialMaximumOffset();
		initialOffsetMin = rectTransform.offsetMin;
		initialOffsetMax = rectTransform.offsetMax;
		targetOffsetMin = reverseDirection ? rectTransform.offsetMin*2 : rectTransform.offsetMin*0.5f;
		targetOffsetMax = reverseDirection ? rectTransform.offsetMax*2 : rectTransform.offsetMax*0.5f;
	}

	private Vector2 InitialMinimumOffset()
	{
		int x = widthSide == WidthSide.LEFT ? Screen.width : 0;
		int y = heightSide == HeightSide.BOTTOM ? Screen.height : 0;
		Vector2 offset = new Vector2(x, y);

		return reverseDirection ? offset*0.5f : offset;
	}

	private Vector2 InitialMaximumOffset()
	{
		int x = widthSide == WidthSide.RIGHT ? -Screen.width : 0;
		int y = heightSide == HeightSide.TOP ? -Screen.height : 0;
		Vector2 offset = new Vector2(x, y);

		return reverseDirection ? offset*0.5f : offset;
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