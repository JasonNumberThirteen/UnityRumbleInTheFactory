public readonly struct PriorityQueueElement<E>
{
	public E Element {get;}
	public float Priority {get;}

	public PriorityQueueElement(E element, float priority)
	{
		Element = element;
		Priority = priority;
	}
}