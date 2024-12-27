using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI), typeof(Timer))]
public class TextUIFader : MonoBehaviour
{
	private TextMeshProUGUI text;
	private Timer timer;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		timer = GetComponent<Timer>();
	}

	private void Update()
	{
		text.color = GetColorWithSetAlpha();
	}

	private Color GetColorWithSetAlpha()
	{
		var color = text.color;

		color.a = GetAlpha();
		
		return color;
	}

	private float GetAlpha() => 1 - timer.ProgressPercent();
}