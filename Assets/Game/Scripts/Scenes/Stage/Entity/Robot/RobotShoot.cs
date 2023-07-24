using UnityEngine;

public class RobotShoot : MonoBehaviour
{
	public GameObject bullet;
	[Min(0f)] public float offsetFromObject = 0.5f;

	protected Animator animator;

	public virtual void FireBullet()
	{
		Vector2 position = transform.position;
		Vector2 bulletDirection = BulletDirection();
		GameObject instance = Instantiate(bullet, position + bulletDirection*offsetFromObject, Quaternion.identity);
		EntityMovement em = instance.GetComponent<EntityMovement>();

		if(em != null)
		{
			em.Direction = bulletDirection;
		}
	}

	protected virtual void Awake() => animator = GetComponent<Animator>();

	protected virtual Vector2 BulletDirection()
	{
		int x = animator.GetInteger("MovementX");
		int y = animator.GetInteger("MovementY");

		return new Vector2(x, y);
	}
}