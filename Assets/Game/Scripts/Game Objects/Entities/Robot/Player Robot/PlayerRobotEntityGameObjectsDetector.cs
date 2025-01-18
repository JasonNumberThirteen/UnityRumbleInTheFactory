using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRobotEntityMovementController))]
public class PlayerRobotEntityGameObjectsDetector : MonoBehaviour
{
	public UnityEvent<List<GameObject>> detectedGameObjectsUpdatedEvent;

	[SerializeField] private Vector2 detectionAreaSize;
	[SerializeField] private LayerMask detectionLayerMask;
	[SerializeField, Min(0f)] private float detectionAreaOffsetFromCenterDistance = 0.1f;

	private PlayerRobotEntityMovementController playerRobotEntityMovementController;

	private readonly Dictionary<Vector2, float> anglesByDirection = new()
	{
		{Vector2.up, 0f},
		{Vector2.down, 180f},
		{Vector2.left, 270f},
		{Vector2.right, 90f}
	};

	private void Awake()
	{
		playerRobotEntityMovementController = GetComponent<PlayerRobotEntityMovementController>();
	}

	private void Update()
	{
		var detectedColliders = Physics2D.OverlapBoxAll(GetDetectionAreaCenter(), detectionAreaSize, GetDetectionAreaAngle(), detectionLayerMask);

		detectedGameObjectsUpdatedEvent?.Invoke(detectedColliders.Where(collider2D => collider2D != null).Select(collider2D => collider2D.gameObject).ToList());
	}

	private float GetDetectionAreaAngle()
	{
		var lastDirection = playerRobotEntityMovementController.GetLastDirection();
		
		return anglesByDirection.ContainsKey(lastDirection) ? anglesByDirection[lastDirection] : 0f;
	}

	private Vector2 GetDetectionAreaCenter() => transform.position + (Vector3)playerRobotEntityMovementController.GetLastDirection()*detectionAreaOffsetFromCenterDistance;
}