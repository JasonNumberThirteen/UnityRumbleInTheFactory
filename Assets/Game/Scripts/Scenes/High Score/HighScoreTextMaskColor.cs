using UnityEngine;
using UnityEngine.UI;

public class HighScoreTextMaskColor : MonoBehaviour
{
	public Color targetColor;
	[Min(0.01f)] public float blinkDelay;

	private Image image;
	private Color startColor;

	private void Awake()
	{
		image = GetComponent<Image>();
		startColor = image.color;
	}

	private void Start() => InvokeRepeating("ChangeColor", blinkDelay, blinkDelay);
	private void ChangeColor() => image.color = (image.color == startColor) ? targetColor : startColor;
}