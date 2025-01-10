using UnityEngine;

public abstract class RobotEntity : MonoBehaviour
{
	private RobotEntityShield robotEntityShield;
	
	public abstract bool IsFriendly();
	public abstract void OnLifeBonusCollected(int lives);

	public void OnRankBonusCollected()
	{
		if(TryGetComponent(out PlayerRobotEntityRankController playerRobotEntityRankController))
		{
			playerRobotEntityRankController.IncreaseRank();
		}
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
	}
}