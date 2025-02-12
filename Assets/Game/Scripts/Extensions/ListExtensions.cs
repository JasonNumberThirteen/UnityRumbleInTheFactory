using System;
using System.Collections.Generic;

public static class ListExtensions
{
	public static void ForEachIndexed<E>(this List<E> list, Action<E, int> action, int counterInitialValue = 0)
	{
		var i = counterInitialValue;

		list.ForEach(playerRobotData => action?.Invoke(playerRobotData, i++));
	}
}