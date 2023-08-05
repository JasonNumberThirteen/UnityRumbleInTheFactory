using UnityEngine;

public class RectTransformAspectRatioAdjuster : MonoBehaviour
{
	public RectTransform canvas;

	private RectTransform rectTransform;
	private Vector2 initialPosition, initialSize;

	private void AdjustSize() => rectTransform.sizeDelta *= SizeOffset();
	private Vector2 SizeOffset() => new Vector2(WidthOffset(), HeightOffset());
	private void Awake() => rectTransform = GetComponent<RectTransform>();
	private float OffsetX() => initialSize.x - rectTransform.sizeDelta.x;
	private float OffsetY() => initialSize.y - rectTransform.sizeDelta.y;
	private float WidthOffset() => canvas.sizeDelta.x / 256;
	private float HeightOffset() => canvas.sizeDelta.y / 224;
	
	private void Start()
	{
		GetInitialValues();
		AdjustSize();
		AdjustPosition();
	}

	private void GetInitialValues()
	{
		initialPosition = rectTransform.anchoredPosition;
		initialSize = rectTransform.sizeDelta;
	}

	private void AdjustPosition()
	{
		float offsetDirection = Mathf.Sign(-initialPosition.x);
		float offsetX = offsetDirection*2*OffsetX();
		float offsetY = offsetDirection*2*OffsetY();
		
		rectTransform.anchoredPosition += new Vector2(offsetX, offsetY);
	}
}