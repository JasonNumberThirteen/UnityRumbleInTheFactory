using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RobotEntity))]
public abstract class RobotEntityRankController : MonoBehaviour
{
	public UnityEvent<RobotRank> rankChangedEvent;
	
	public RobotRank CurrentRank {get; set;}

	protected RobotEntity robotEntity;

	public abstract void IncreaseRank();

	protected virtual void Awake()
	{
		robotEntity = GetComponent<RobotEntity>();
	}
}