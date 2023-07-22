using UnityEngine;

public class BricksTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect(GameObject sender) => Destroy(gameObject);
}