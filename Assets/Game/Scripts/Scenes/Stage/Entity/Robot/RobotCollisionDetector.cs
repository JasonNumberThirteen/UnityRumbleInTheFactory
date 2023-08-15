using UnityEngine;

public class RobotCollisionDetector : MonoBehaviour
{
	public LayerMask detectionLayers;
	
	private Collider2D c2D;

	public Collider2D OverlapBox() => Physics2D.OverlapBox(c2D.bounds.center, c2D.bounds.size, 0f, detectionLayers);
	public Collider2D[] OverlapBoxAll() => Physics2D.OverlapBoxAll(c2D.bounds.center, c2D.bounds.size, 0f, detectionLayers);

	public void AdjustRotation(Vector2 direction)
	{
		if(direction == Vector2.up)
		{
			gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
		}
		else if(direction == Vector2.down)
		{
			gameObject.transform.rotation = Quaternion.Euler(Vector3.forward*180);
		}
		else if(direction == Vector2.left)
		{
			gameObject.transform.rotation = Quaternion.Euler(Vector3.forward*90);
		}
		else if(direction == Vector2.right)
		{
			gameObject.transform.rotation = Quaternion.Euler(Vector3.forward*270);
		}
	}

	private void Awake() => c2D = GetComponent<Collider2D>();
}