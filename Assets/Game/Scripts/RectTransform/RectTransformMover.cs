using UnityEngine;

public class RectTransformMover : MonoBehaviour
{
	protected RectTransform rectTransform;

	public void AddPositionX(float x) => SetPositionX(rectTransform.anchoredPosition.x + x);
	public void AddPositionY(float y) => SetPositionY(rectTransform.anchoredPosition.y + y);
	public void SetPositionX(float x) => rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
	public void SetPositionY(float y) => rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);

	protected virtual void Awake() => rectTransform = GetComponent<RectTransform>();
}