using UnityEngine;
using UnityEngine.Events;

public abstract class EntityRankController<T> : MonoBehaviour where T : RobotRank
{
	public UnityEvent<T> rankChangedEvent;
	
	public T CurrentRank {get; set;}
}