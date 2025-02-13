using Random = UnityEngine.Random;
using System;

public static class ArrayExtensions
{
	public static E GetRandomElement<E>(this E[] array) => array.Length > 0 ? array[Random.Range(0, array.Length)] : default;
	
	public static void ForEachIndexed<E>(this E[] array, Action<E, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		array.ForEach(playerRobotData => action?.Invoke(playerRobotData, i++));
	}

	public static void ForEach<E>(this E[] array, Action<E> action)
	{
		foreach (var element in array)
		{
			action?.Invoke(element);
		}
	}
}