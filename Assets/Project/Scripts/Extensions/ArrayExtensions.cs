using Random = UnityEngine.Random;
using System;

public static class ArrayExtensions
{
	public static T GetElementAt<T>(this T[] array, int index) => array.Length > 0 && index >= 0 && index < array.Length ? array[index] : default;
	public static T GetRandomElement<T>(this T[] array) => array.Length > 0 ? array[Random.Range(0, array.Length)] : default;
	
	public static void ForEachIndexed<T>(this T[] array, Action<T, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		array.ForEach(element => action?.Invoke(element, i++));
	}

	public static void ForEach<T>(this T[] array, Action<T> action)
	{
		foreach (var element in array)
		{
			action?.Invoke(element);
		}
	}
}