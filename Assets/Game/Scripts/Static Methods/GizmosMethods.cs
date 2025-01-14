using System;
using UnityEngine;

public static class GizmosMethods
{
	public static void OperateOnGizmos(Action action, Color color)
	{
		var previousColor = Gizmos.color;

		Gizmos.color = color;

		action?.Invoke();

		Gizmos.color = previousColor;
	}
}