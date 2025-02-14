using UnityEngine;

public class RobotEntityTriggerEventsSender : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.TryGetComponent(out ITriggerableOnEnter triggerableOnEnter))
		{
			triggerableOnEnter.TriggerOnEnter(gameObject);
		}
	}
}