using System.Linq;
using System.Collections.Generic;

public class PriorityQueue<E>
{
	private List<PriorityQueueElement<E>> list = new();

	public int Count => list.Count;

	public void Clear()
	{
		list.Clear();
	}

	public void Enqueue(PriorityQueueElement<E> priorityQueueElement)
	{
		list.Add(priorityQueueElement);

		list = list.OrderBy(element => element.Priority).ToList();
	}

	public E Dequeue()
	{
		if(list.Count == 0)
		{
			return default;
		}
		
		var element = list.First();

		list.RemoveAt(0);

		return element.Element;
	}
}