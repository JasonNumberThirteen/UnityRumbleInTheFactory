using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransformPositionController))]
public class MainMenuOptionsCursorImageUI : MonoBehaviour
{
	private Image image;
	private RectTransformPositionController rectTransformPositionController;

	public void SetActive(bool active)
	{
		image.enabled = active;
	}

	public void SetPositionRelativeToOption(Option option)
	{
		rectTransformPositionController.SetPositionY(option.GetPosition().y);
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