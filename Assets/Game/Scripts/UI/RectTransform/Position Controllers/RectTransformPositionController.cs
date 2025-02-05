using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectTransformPositionController : MonoBehaviour
{
	protected RectTransform rectTransform;

	public float GetPositionX() => GetPosition().x;
	public float GetPositionY() => GetPosition().y;
	public Vector2 GetPosition() => rectTransform.anchoredPosition;

	public void AddPositionX(float x)
	{
		SetPositionX(rectTransform.anchoredPosition.x + x);
	}

	public void AddPositionY(float y)
	{
		SetPositionY(rectTransform.anchoredPosition.y + y);
	}

	public void AddPosition(Vector2 position)
	{
		rectTransform.anchoredPosition += position;
	}

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