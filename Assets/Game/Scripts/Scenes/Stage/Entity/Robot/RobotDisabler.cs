using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class RobotDisabler : MonoBehaviour
{
	private Behaviour[] behaviours;
	
	public void SetBehavioursActive(bool active)
	{
		if(behaviours == null)
		{
			return;
		}
		
		foreach (var behaviour in behaviours)
		{
			behaviour.enabled = active;
		}
	}

	protected abstract List<Behaviour> GetBehaviours();

	private void Awake()
	{
		behaviours = GetBehaviours()?.Where(behaviour => behaviour.TryGetComponent(out Behaviour _)).ToArray();
	}
}