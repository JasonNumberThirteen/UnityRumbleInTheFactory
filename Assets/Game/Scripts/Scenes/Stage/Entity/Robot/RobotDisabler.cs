using UnityEngine;

public class RobotDisabler : MonoBehaviour
{
	[SerializeField] private Behaviour[] behaviours;
	
	public void SetBehavioursActive(bool active)
	{
		foreach (var behaviour in behaviours)
		{
			behaviour.enabled = active;
		}
	}
}