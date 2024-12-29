using System;
using UnityEngine;

[Serializable]
public class RobotRank
{
	[SerializeField, Min(1)] private int health;
	[SerializeField, Min(1)] private int damage;
	[SerializeField, Min(0.01f)] private float movementSpeed;
	[SerializeField, Min(0.01f)] private float bulletSpeed;
	[SerializeField] private bool canDestroyMetal;

	public int GetHealth() => health;
	public int GetDamage() => damage;
	public float GetMovementSpeed() => movementSpeed;
	public float GetBulletSpeed() => bulletSpeed;
	public bool CanDestroyMetal() => canDestroyMetal;
}