using System;
using UnityEngine;

[Serializable]
public class BulletStats
{
	[SerializeField, Min(1)] private int damage = 1;
	[SerializeField, Min(0.01f)] private float movementSpeed = 6.5f;
	[SerializeField] private bool canDestroyMetal;

	public void SetValues(int damage, float movementSpeed, bool canDestroyMetal)
	{
		this.damage = damage;
		this.movementSpeed = movementSpeed;
		this.canDestroyMetal = canDestroyMetal;
	}

	public int GetDamage() => damage;
	public float GetMovementSpeed() => movementSpeed;
	public bool CanDestroyMetal() => canDestroyMetal;
}