using UnityEngine;

[RequireComponent(typeof(RobotEntity))]
public abstract class RobotEntityRankController<T> : EntityRankController<T> where T : RobotRank
{
	protected RobotEntity robotEntity;

	public abstract void IncreaseRank();

	protected virtual void Awake()
	{
		robotEntity = GetComponent<RobotEntity>();
	}
}