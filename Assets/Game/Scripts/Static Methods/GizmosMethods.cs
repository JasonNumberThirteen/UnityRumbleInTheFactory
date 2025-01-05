using System;
using UnityEngine;

public class GizmosMethods : MonoBehaviour
{
	public static void OperateOnGizmos(Action action, Color color)
	{
		var previousColor = Gizmos.color;

		Gizmos.color = color;

		action?.Invoke();

		Gizmos.color = previousColor;
	}
}