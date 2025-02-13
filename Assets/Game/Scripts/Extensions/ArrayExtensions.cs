using Random = UnityEngine.Random;
using System;

public static class ArrayExtensions
{
	public static E GetRandomElement<E>(this E[] array) => array.Length > 0 ? array[Random.Range(0, array.Length)] : default;
	
	public static void ForEach<E>(this E[] array, Action<E> action)
	{
		foreach (var element in array)
		{
			action?.Invoke(element);
		}
	}
}