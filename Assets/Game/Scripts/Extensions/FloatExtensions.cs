using UnityEngine;

public static class FloatExtensions
{
	public static float GetClampedValue(this float @float, float min, float max) => Mathf.Clamp(@float, min, max);
}