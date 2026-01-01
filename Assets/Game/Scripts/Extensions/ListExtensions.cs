using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;

public static class ListExtensions
{
	public static T GetRandomElement<T>(this List<T> list) => list.Count > 0 ? list[Random.Range(0, list.Count)] : default;
	
	public static void ForEachIndexed<T>(this List<T> list, Action<T, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		list.ForEach(element => action?.Invoke(element, i++));
	}

	public static void AddIfNotExists<T>(this List<T> list, T element)
	{
		if(!list.Contains(element))
		{
			list.Add(element);
		}
	}
}