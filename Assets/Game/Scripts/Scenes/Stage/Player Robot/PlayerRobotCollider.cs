using UnityEngine;

public class PlayerRobotCollider : MonoBehaviour
{
	private PlayerRobotMovement movement;

	private void Awake() => movement = GetComponent<PlayerRobotMovement>();

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.CompareTag("Slippery Floor"))
		{
			SetSliding(true);
		}
		else if(collider.CompareTag("Bonus"))
		{
			Destroy(collider.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.CompareTag("Slippery Floor"))
		{
			SetSliding(false);
		}
	}

	private void SetSliding(bool isSliding) => movement.IsSliding = isSliding;
}