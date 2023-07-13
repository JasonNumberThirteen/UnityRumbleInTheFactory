using UnityEngine;

public class RobotShoot : MonoBehaviour
{
	public GameObject bullet;
	[Min(0f)] public float offsetFromObject = 0.5f;

	protected Animator animator;

	public virtual void FireBullet()
	{
		GameObject instance = Instantiate(bullet, transform.position + BulletPositionOffset()*offsetFromObject, Quaternion.identity);
		EntityMovement em = instance.GetComponent<EntityMovement>();

		if(em != null)
		{
			em.Direction = BulletDirection();
		}
	}

	protected virtual void Awake() => animator = GetComponent<Animator>();

	protected virtual Vector3 BulletPositionOffset()
	{
		Vector2 direction = BulletDirection();

		return new Vector3(direction.x, direction.y, 0f);
	}

	protected virtual Vector2 BulletDirection()
	{
		int x = animator.GetInteger("MovementX");
		int y = animator.GetInteger("MovementY");

		return new Vector2(x, y);
	}
}