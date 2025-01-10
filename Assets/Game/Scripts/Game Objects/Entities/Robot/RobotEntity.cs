using UnityEngine;

[RequireComponent(typeof(RobotEntityRankController))]
public abstract class RobotEntity : MonoBehaviour
{
	private RobotEntityShield robotEntityShield;
	private RobotEntityRankController robotEntityRankController;
	
	public abstract bool IsFriendly();
	public abstract void OnLifeBonusCollected(int lives);

	public void OnRankBonusCollected()
	{
		robotEntityRankController.IncreaseRank();
	}

	public void ActivateShield(float duration)
	{
		if(robotEntityShield != null)
		{
			robotEntityShield.ActivateShield(duration);
		}
	}

	protected virtual void Awake()
	{
		robotEntityShield = GetComponentInChildren<RobotEntityShield>();
		robotEntityRankController = GetComponent<RobotEntityRankController>();
	}
}