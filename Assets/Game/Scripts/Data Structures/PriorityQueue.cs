using System.Linq;
using System.Collections.Generic;

public class PriorityQueue<E>
{
	private List<PriorityQueueElement<E>> list = new();

	public int Count => list.Count;
	public E Dequeue() => list.PopFirst().Element;

	public void Clear()
	{
		list.Clear();
	}

	public void Enqueue(PriorityQueueElement<E> priorityQueueElement)
	{
		list.Add(priorityQueueElement);

		list = list.OrderBy(element => element.Priority).ToList();
	}
}