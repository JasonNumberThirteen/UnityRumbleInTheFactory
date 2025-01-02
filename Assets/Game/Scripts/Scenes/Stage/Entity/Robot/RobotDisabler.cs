using System.Collections.Generic;
using UnityEngine;

public abstract class RobotDisabler : MonoBehaviour
{
	public void SetComponentsActive(bool active)
	{
		GetComponents()?.ForEach(component => component.enabled = active);
	}

	protected abstract List<Behaviour> GetComponents();
}