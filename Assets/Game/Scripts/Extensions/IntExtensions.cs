using System;
using UnityEngine;

public static class IntExtensions
{
	public static int GetClampedValue(this int @int, int min, int max) => Mathf.Clamp(@int, min, max);
	public static T ToEnumValue<T>(this int @int) where T : Enum => (T)Enum.ToObject(typeof(T), @int);
}