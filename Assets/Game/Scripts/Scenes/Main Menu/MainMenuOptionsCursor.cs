using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransformPositionController))]
public class MainMenuOptionsCursor : MonoBehaviour
{
	private Image image;
	private RectTransformPositionController rectTransformPositionController;

	public void SetActive(bool active)
	{
		image.enabled = active;
	}

	public void SetPositionY(float y)
	{
		rectTransformPositionController.SetPositionY(y);
	}

	private void Awake()
	{
		image = GetComponent<Image>();
		rectTransformPositionController = GetComponent<RectTransformPositionController>();
	}

	private void Start()
	{
		SetActive(false);
	}
}