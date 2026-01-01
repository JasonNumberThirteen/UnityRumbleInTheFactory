using System.Linq;
using UnityEngine;

public class RobotEntityDisabler : MonoBehaviour
{
	[SerializeField] private Behaviour[] behaviours;

	public void SetBehavioursActive(bool active, Behaviour[] excludedBehaviours = null)
	{
		behaviours.ForEach(behaviour =>
		{
			if(behaviour != null && (excludedBehaviours == null || !excludedBehaviours.Contains(behaviour)))
			{
				behaviour.enabled = active;
			}
		});
	}
}