using System;
using UnityEngine;

[Serializable]
public class RobotRank
{
	[SerializeField, Min(1)] private int health;
	[SerializeField, Min(0f)] private float movementSpeed;
	[SerializeField] private BulletStats bulletStats = new();
	[SerializeField] private RuntimeAnimatorController runtimeAnimatorController;

	public int GetHealth() => health;
	public float GetMovementSpeed() => movementSpeed;
	public int GetDamage() => bulletStats.GetDamage();
	public float GetBulletMovementSpeed() => bulletStats.GetMovementSpeed();
	public bool CanDestroyMetal() => bulletStats.CanDestroyMetal();
	public RuntimeAnimatorController GetRuntimeAnimatorController() => runtimeAnimatorController;
}