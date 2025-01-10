using System;
using UnityEngine;

[Serializable]
public class BulletStats
{
	[SerializeField, Min(1)] private int damage;
	[SerializeField, Min(0.01f)] private float bulletSpeed;
	[SerializeField] private bool canDestroyMetal;

	public BulletStats(int damage, float bulletSpeed, bool canDestroyMetal)
	{
		this.damage = damage;
		this.bulletSpeed = bulletSpeed;
		this.canDestroyMetal = canDestroyMetal;
	}

	public int GetDamage() => damage;
	public float GetBulletSpeed() => bulletSpeed;
	public bool CanDestroyMetal() => canDestroyMetal;
}