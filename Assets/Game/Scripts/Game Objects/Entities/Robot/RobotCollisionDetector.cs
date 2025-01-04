using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RobotCollisionDetector : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;
	
	private Collider2D c2D;

	public Collider2D OverlapBox() => Physics2D.OverlapBox(GetColliderCenter(), GetColliderSize(), 0f, layerMask);
	public Collider2D[] OverlapBoxAll() => Physics2D.OverlapBoxAll(GetColliderCenter(), GetColliderSize(), 0f, layerMask);

	private Vector2 GetColliderCenter() => c2D.bounds.center;
	private Vector2 GetColliderSize() => c2D.bounds.size;

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
	}
}