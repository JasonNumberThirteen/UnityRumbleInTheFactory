using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RobotCollisionDetector : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color detectedColliderGizmosColor = Color.red;
	
	private Collider2D c2D;

	public Collider2D OverlapBox() => Physics2D.OverlapBox(GetColliderCenter(), GetColliderSize(), 0f, layerMask);
	public Collider2D[] OverlapBoxAll() => Physics2D.OverlapBoxAll(GetColliderCenter(), GetColliderSize(), 0f, layerMask);

	private Vector2 GetColliderCenter() => c2D.bounds.center;
	private Vector2 GetColliderSize() => c2D.bounds.size;

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
	}

	private void OnDrawGizmos()
	{
		if(drawGizmos)
		{
			DrawDetectedColliders();
		}
	}

	private void DrawDetectedColliders()
	{
		if(c2D == null)
		{
			c2D = GetComponent<Collider2D>();
		}
		
		GizmosMethods.OperateOnGizmos(() =>
		{
			var colliders = OverlapBoxAll();
			
			foreach (var collider in colliders)
			{
				Gizmos.DrawWireCube(collider.transform.position, collider.bounds.size);
			}
		}, detectedColliderGizmosColor);
	}
}