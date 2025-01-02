using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageUIColorBlinker : MonoBehaviour
{
	[SerializeField] private Color targetColor;
	[SerializeField, Min(0.01f)] private float blinkDelay;

	private Image image;
	private Color initialColor;

	private void Awake()
	{
		image = GetComponent<Image>();
		initialColor = image.color;
	}

	private void Start()
	{
		InvokeRepeating(nameof(SwitchColor), blinkDelay, blinkDelay);
	}

	private void SwitchColor()
	{
		var imageHasInitialColor = image.color == initialColor;
		
		image.color = imageHasInitialColor ? targetColor : initialColor;
	}
}