using TMPro;
using UnityEngine;

public class TextUIFader : MonoBehaviour
{
	private TextMeshProUGUI text;
	private Timer timer;

	private void Update() => text.color = TextColor();
	private float Alpha() => 1 - timer.ProgressPercent();

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		timer = GetComponent<Timer>();
	}

	private Color TextColor()
	{
		Color color = text.color;

		color.a = Alpha();
		
		return color;
	}
}