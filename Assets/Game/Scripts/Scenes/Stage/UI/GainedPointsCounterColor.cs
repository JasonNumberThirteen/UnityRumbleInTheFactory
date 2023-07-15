using TMPro;
using UnityEngine;

public class GainedPointsCounterColor : MonoBehaviour
{
	private TextMeshProUGUI text;
	private Timer timer;

	private void Awake()
	{
		text = GetComponent<TextMeshProUGUI>();
		timer = GetComponent<Timer>();
	}

	private void Update() => text.color = TextColor();

	private Color TextColor()
	{
		Color currentColor = text.color;
		
		return new Color(currentColor.r, currentColor.g, currentColor.b, 1 - timer.ProgressPercent());
	}
}