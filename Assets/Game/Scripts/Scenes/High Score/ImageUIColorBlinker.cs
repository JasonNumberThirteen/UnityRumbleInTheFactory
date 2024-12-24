using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageUIColorBlinker : MonoBehaviour
{
	[SerializeField] private Color targetColor;
	[SerializeField, Min(0.01f)] private float blinkDelay;

	private Image image;
	private Color startColor;

	private void Awake()
	{
		image = GetComponent<Image>();
		startColor = image.color;
	}

	private void Start() => InvokeRepeating(nameof(ChangeColor), blinkDelay, blinkDelay);
	private void ChangeColor() => image.color = (image.color == startColor) ? targetColor : startColor;
}