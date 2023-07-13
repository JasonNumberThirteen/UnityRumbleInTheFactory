using UnityEngine;

public class BricksTrigger : MonoBehaviour, ITriggerable
{
	public void TriggerEffect() => Destroy(gameObject);
}