using UnityEngine;
using System.Collections;

public class EnemyRobotShoot : MonoBehaviour
{
	public GameObject bullet;
	[Min(0f)] public float offsetFromObject = 0.5f;
	[Min(0.01f)] public float fireDelay = 1f;

	private Animator animator;

	public void FireBullet()
	{
		GameObject instance = Instantiate(bullet, transform.position + BulletPositionOffset()*offsetFromObject, Quaternion.identity);
		EntityMovement em = instance.GetComponent<EntityMovement>();

		if(em != null)
		{
			em.Direction = BulletDirection();
		}
	}

	private void Awake() => animator = GetComponent<Animator>();
	private void Start() => InvokeRepeating("FireBullet", fireDelay, fireDelay);

	private Vector3 BulletPositionOffset()
	{
		Vector2 direction = BulletDirection();

		return new Vector3(direction.x, direction.y, 0f);
	}

	private Vector2 BulletDirection()
	{
		int x = animator.GetInteger("MovementX");
		int y = animator.GetInteger("MovementY");

		return new Vector2(x, y);
	}
}