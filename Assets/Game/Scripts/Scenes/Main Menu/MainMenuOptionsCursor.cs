using UnityEngine;

[RequireComponent(typeof(RectTransformPositionController))]
public class MainMenuOptionsCursor : MonoBehaviour
{
	private RectTransformPositionController rectTransformPositionController;
	
	public void SetActive(bool active)
	{
		gameObject.SetActive(active);
	}

	public void SetPositionY(float y)
	{
		rectTransformPositionController.SetPositionY(y);	
	}

	private void Awake()
	{
		rectTransformPositionController = GetComponent<RectTransformPositionController>();
	}
}