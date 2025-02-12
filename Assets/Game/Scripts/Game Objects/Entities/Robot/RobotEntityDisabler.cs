using UnityEngine;

public class RobotEntityDisabler : MonoBehaviour
{
	[SerializeField] private Behaviour[] behaviours;
	
	public void SetBehavioursActive(bool active)
	{
		behaviours.ForEach(behaviour =>
		{
			if(behaviour != null)
			{
				behaviour.enabled = active;
			}
		});
	}
}