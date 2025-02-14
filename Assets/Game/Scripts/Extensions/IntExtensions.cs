using UnityEngine;

public static class IntExtensions
{
	public static int GetClampedValue(this int @int, int min, int max) => Mathf.Clamp(@int, min, max);
}