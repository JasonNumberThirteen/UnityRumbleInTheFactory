using System;

public static class ArrayExtensions
{
	public static void ForEach<E>(this E[] array, Action<E> action)
	{
		if(array == null)
		{
			return;
		}
		
		foreach (var element in array)
		{
			action?.Invoke(element);
		}
	}
}