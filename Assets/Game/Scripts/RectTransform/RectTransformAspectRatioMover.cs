using UnityEngine;

public class RectTransformAspectRatioMover : RectTransformMover
{
	public RectTransform canvas;

	private Vector2 Difference() => new Vector2(DifferenceX(), DifferenceY());
	private float DifferenceX() => canvas.sizeDelta.x / 256;
	private float DifferenceY() => canvas.sizeDelta.y / 224;

	private void Start()
	{
		Vector2 position = rectTransform.anchoredPosition*Difference();

		SetPositionX(position.x);
		SetPositionY(position.y);
	}
}