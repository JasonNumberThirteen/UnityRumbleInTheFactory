using UnityEngine;

public abstract class Robot : MonoBehaviour
{
	private RobotShield robotShield;
	
	public abstract bool IsFriendly();

	public void ActivateShield(float duration)
	{
		if(robotShield != null)
		{
			robotShield.ActivateShield(duration);
		}
	}

	private void Awake()
	{
		robotShield = GetComponentInChildren<RobotShield>();
	}
}