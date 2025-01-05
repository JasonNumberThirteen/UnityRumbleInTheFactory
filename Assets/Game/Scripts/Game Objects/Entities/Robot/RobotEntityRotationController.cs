using System.Collections.Generic;
using UnityEngine;

public class RobotEntityRotationController : MonoBehaviour
{
	private readonly Dictionary<Vector2, Vector3> eulerAnglesByDirection = new()
	{
		{Vector2.up, Vector3.zero},
		{Vector2.down, Vector3.down},
		{Vector2.left, Vector3.left},
		{Vector2.right, Vector3.right}
	};

	public void RotateByDirection(Vector2 direction)
	{
		if(eulerAnglesByDirection.ContainsKey(direction))
		{
			transform.rotation = Quaternion.Euler(eulerAnglesByDirection[direction]);
		}
	}
}