using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RobotEntityCollisionDetector : MonoBehaviour
{
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color detectedColliderGizmosColor = Color.red;
	
	private Collider2D c2D;
	private GameObject parentGO;

	private readonly Dictionary<Vector2, Vector3> eulerAnglesByDirection = new()
	{
		{Vector2.up, Vector3.zero},
		{Vector2.down, Vector3.forward*180},
		{Vector2.left, Vector3.forward*90},
		{Vector2.right, Vector3.forward*270}
	};

	public void AdjustRotationIfPossible(Vector2 direction)
	{
		if(eulerAnglesByDirection.ContainsKey(direction))
		{
			transform.rotation = Quaternion.Euler(eulerAnglesByDirection[direction]);
		}
	}

	public Collider2D OverlapBox() => Physics2D.OverlapBox(GetColliderCenter(), GetColliderSize(), 0f, layerMask);
	public Collider2D[] OverlapBoxAll() => Physics2D.OverlapBoxAll(GetColliderCenter(), GetColliderSize(), 0f, layerMask).Where(collider => collider.gameObject != parentGO).ToArray();

	private Vector2 GetColliderCenter() => c2D.bounds.center;
	private Vector2 GetColliderSize() => c2D.bounds.size;

	private void Awake()
	{
		c2D = GetComponent<Collider2D>();
		parentGO = transform.parent.gameObject;
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
				Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
			}
		}, detectedColliderGizmosColor);
	}
}