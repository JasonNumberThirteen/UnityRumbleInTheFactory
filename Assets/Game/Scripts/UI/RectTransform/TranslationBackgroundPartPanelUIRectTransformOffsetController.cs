using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TranslationBackgroundPartPanelUIRectTransformOffsetController : MonoBehaviour
{
	[SerializeField] private HorizontalDirection horizontalDirection;
	[SerializeField] private VerticalDirection verticalDirection;
	[SerializeField] private bool moveFromCenterToEdges;

	private Timer timer;
	private RectTransform rectTransform;
	private Vector2 initialOffsetMin;
	private Vector2 initialOffsetMax;
	private Vector2 targetOffsetMin;
	private Vector2 targetOffsetMax;
	private GameResolutionChangeDetector gameResolutionChangeDetector;

	public void Setup(Timer timer)
	{
		this.timer = timer;
	}

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		gameResolutionChangeDetector = ObjectMethods.FindComponentOfType<GameResolutionChangeDetector>();

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
			if(gameResolutionChangeDetector != null)
			{
				gameResolutionChangeDetector.resolutionWasChangedEvent.AddListener(OnResolutionWasChanged);
			}
		}
		else
		{
			if(gameResolutionChangeDetector != null)
			{
				gameResolutionChangeDetector.resolutionWasChangedEvent.RemoveListener(OnResolutionWasChanged);
			}
		}
	}

	private void OnResolutionWasChanged()
	{
		UpdateOffsetValues();
	}

	private void Start()
	{
		UpdateOffsetValues();
	}

	private void UpdateOffsetValues()
	{
		initialOffsetMin = initialOffsetMax = rectTransform.offsetMin = rectTransform.offsetMax = GetInitialOffset();
		targetOffsetMin = GetTargetOffset(initialOffsetMin);
		targetOffsetMax = GetTargetOffset(initialOffsetMax);
	}

	private Vector2 GetInitialOffset()
	{
		var offset = new Vector2(GetInitialOffsetX(), GetInitialOffsetY());

		return moveFromCenterToEdges ? offset*0.5f : offset;
	}

	private float GetInitialOffsetX()
	{
		var screenWidth = Screen.width;
		
		return horizontalDirection switch
		{
			HorizontalDirection.Left => screenWidth,
			HorizontalDirection.Right => -screenWidth,
			_ => 0
		};
	}

	private float GetInitialOffsetY()
	{
		var screenHeight = Screen.height;
		
		return verticalDirection switch
		{
			VerticalDirection.Up => -screenHeight,
			VerticalDirection.Down => screenHeight,
			_ => 0
		};
	}

	private void Update()
	{
		if(timer.TimerWasStarted && !ReachedTargetPosition())
		{
			SetOffset();
		}
	}
	
	private void SetOffset()
	{
		rectTransform.offsetMin = GetOffset(initialOffsetMin, GetOffsetDifference(targetOffsetMin, initialOffsetMin));
		rectTransform.offsetMax = GetOffset(initialOffsetMax, GetOffsetDifference(targetOffsetMax, initialOffsetMax));
	}

	private Vector2 GetOffset(Vector2 initialOffset, Vector2 offsetDifference)
	{
		var percent = timer.GetProgressPercent();
		var x = GetOffsetCoordinate(initialOffset.x, offsetDifference.x, percent);
		var y = GetOffsetCoordinate(initialOffset.y, offsetDifference.y, percent);

		return new(x, y);
	}

	private Vector2 GetTargetOffset(Vector2 initialOffset) => moveFromCenterToEdges ? initialOffset*2 : initialOffset*0.5f;
	private Vector2 GetOffsetDifference(Vector2 targetOffset, Vector2 initialOffset) => targetOffset - initialOffset;
	private bool ReachedTargetPosition() => rectTransform.offsetMin == targetOffsetMin && rectTransform.offsetMax == targetOffsetMax;
	private float GetOffsetCoordinate(float initialOffsetCoordinate, float offsetDifferenceCoordinate, float percent) => initialOffsetCoordinate + offsetDifferenceCoordinate*percent;
}