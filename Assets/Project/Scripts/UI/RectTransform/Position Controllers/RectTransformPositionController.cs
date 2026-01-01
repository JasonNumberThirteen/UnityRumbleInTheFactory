using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectTransformPositionController : MonoBehaviour
{
	protected RectTransform rectTransform;

	public Vector2 GetPosition() => rectTransform.anchoredPosition;

	public void SetPositionX(float x)
	{
		SetPosition(new Vector2(x, rectTransform.anchoredPosition.y));
	}

	public void SetPositionY(float y)
	{
		SetPosition(new Vector2(rectTransform.anchoredPosition.x, y));
	}

	public void SetPosition(Vector2 position)
	{
		rectTransform.anchoredPosition = position;
	}

	protected virtual void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}
}