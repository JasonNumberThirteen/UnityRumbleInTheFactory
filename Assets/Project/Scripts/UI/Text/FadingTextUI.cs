using UnityEngine;

[RequireComponent(typeof(Timer))]
public class FadingTextUI : TextUI
{
	private Timer timer;

	protected override void Awake()
	{
		base.Awake();

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

	private float GetAlpha() => 1 - timer.GetProgressPercent();
}