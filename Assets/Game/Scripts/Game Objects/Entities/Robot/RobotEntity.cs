using UnityEngine;

public abstract class RobotEntity : MonoBehaviour
{
	private RobotEntityShield robotEntityShield;
	
	public abstract bool IsFriendly();

	public void ActivateShield(float duration)
	{
		if(robotEntityShield != null)
		{
			robotEntityShield.ActivateShield(duration);
		}
	}

	private void Awake()
	{
		robotEntityShield = GetComponentInChildren<RobotEntityShield>();
	}
}