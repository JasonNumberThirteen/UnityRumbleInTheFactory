using System;
using UnityEngine;

[Serializable]
public class RobotRank
{
	[SerializeField, Min(1)] private int health;
	[SerializeField, Min(0.01f)] private float movementSpeed;
	[SerializeField] private BulletStats bulletStats;
	[SerializeField] private RuntimeAnimatorController runtimeAnimatorController;

	public int GetHealth() => health;
	public float GetMovementSpeed() => movementSpeed;
	public int GetDamage() => bulletStats.GetDamage();
	public float GetBulletSpeed() => bulletStats.GetBulletSpeed();
	public bool CanDestroyMetal() => bulletStats.CanDestroyMetal();
	public RuntimeAnimatorController GetRuntimeAnimatorController() => runtimeAnimatorController;
}