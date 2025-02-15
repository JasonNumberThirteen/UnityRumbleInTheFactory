using Random = UnityEngine.Random;
using System;
using System.Linq;
using System.Collections.Generic;

public static class ListExtensions
{
	public static T GetRandomElement<T>(this List<T> list) => list.Count > 0 ? list[Random.Range(0, list.Count)] : default;
	
	public static void ForEachIndexed<T>(this List<T> list, Action<T, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		list.ForEach(playerRobotData => action?.Invoke(playerRobotData, i++));
	}

	public static T PopFirst<T>(this List<T> list)
	{
		if(list.Count == 0)
		{
			return default;
		}
		
		var firstElement = list.First();

		list.RemoveAt(0);

		return firstElement;
	}
}