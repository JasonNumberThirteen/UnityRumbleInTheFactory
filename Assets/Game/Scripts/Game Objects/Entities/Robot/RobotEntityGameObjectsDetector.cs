using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RobotEntityMovementController))]
public class RobotEntityGameObjectsDetector : MonoBehaviour
{
	public UnityEvent<List<GameObject>> detectedGameObjectsUpdatedEvent;

	[SerializeField] private Vector2 detectionAreaSize;
	[SerializeField] private LayerMask detectionLayerMask;
	[SerializeField, Min(0f)] private float detectionAreaOffsetFromCenterDistance = 0.1f;
	[SerializeField] private bool drawGizmos = true;
	[SerializeField] private Color detectionAreaGizmosColor = Color.green;

	private RobotEntityMovementController robotEntityMovementController;

	private readonly Dictionary<Vector2, float> anglesByDirection = new()
	{
		{Vector2.up, 0f},
		{Vector2.down, 180f},
		{Vector2.left, 270f},
		{Vector2.right, 90f}
	};

	private void Awake()
	{
		robotEntityMovementController = GetComponent<RobotEntityMovementController>();
	}

	private void Update()
	{
		var detectedColliders = Physics2D.OverlapBoxAll(GetDetectionAreaCenter(), detectionAreaSize, GetDetectionAreaAngle(), detectionLayerMask);

		detectedGameObjectsUpdatedEvent?.Invoke(detectedColliders.Where(collider2D => collider2D != null).Select(collider2D => collider2D.gameObject).Where(go => go != gameObject).ToList());
	}

	private float GetDetectionAreaAngle()
	{
		var lastDirection = robotEntityMovementController.GetLastDirection();
		
		return anglesByDirection.ContainsKey(lastDirection) ? anglesByDirection[lastDirection] : 0f;
	}

	private void OnDrawGizmos()
	{
		if(!drawGizmos)
		{
			return;
		}
		
		if(robotEntityMovementController == null)
		{
			robotEntityMovementController = GetComponent<RobotEntityMovementController>();
		}

		GizmosMethods.OperateOnGizmos(() =>
		{
			var originalMatrix = Gizmos.matrix;
			var rotation = Quaternion.Euler(0, 0, GetDetectionAreaAngle());

			Gizmos.matrix = Matrix4x4.TRS((Vector3)GetDetectionAreaCenter(), rotation, Vector3.one);

			Gizmos.DrawWireCube(Vector3.zero, detectionAreaSize);

			Gizmos.matrix = originalMatrix;
		}, detectionAreaGizmosColor);
	}

	private Vector2 GetDetectionAreaCenter() => transform.position + (Vector3)robotEntityMovementController.GetLastDirection()*detectionAreaOffsetFromCenterDistance;
}