using UnityEngine;

public class RobotEntityDisabler : MonoBehaviour
{
	[SerializeField] private Behaviour[] behaviours;
	
	public void SetBehavioursActive(bool active)
	{
		foreach (var behaviour in behaviours)
		{
			if(behaviour != null)
			{
				behaviour.enabled = active;
			}
		}
	}
}