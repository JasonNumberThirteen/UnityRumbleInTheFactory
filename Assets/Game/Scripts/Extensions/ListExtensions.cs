using Random = UnityEngine.Random;
using System;
using System.Linq;
using System.Collections.Generic;

public static class ListExtensions
{
	public static E GetRandomElement<E>(this List<E> list) => list != null && list.Count > 0 ? list[Random.Range(0, list.Count)] : default;
	
	public static void ForEachIndexed<E>(this List<E> list, Action<E, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		list.ForEach(playerRobotData => action?.Invoke(playerRobotData, i++));
	}

	public static E PopFirst<E>(this List<E> list)
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