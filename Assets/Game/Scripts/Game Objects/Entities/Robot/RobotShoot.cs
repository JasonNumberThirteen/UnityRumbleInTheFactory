using UnityEngine;

public class RobotShoot : MonoBehaviour
{
	public GameObject bullet;
	[Min(0f)] public float offsetFromObject = 0.5f;

	protected Animator animator;
	protected StageSoundManager stageSoundManager;

	public virtual void FireBullet()
	{
		SetupBullet(BulletInstance());
	}

	protected GameObject BulletInstance() => Instantiate(bullet, BulletPosition(), Quaternion.identity);
	protected Vector2 BulletPosition() => (Vector2)transform.position + BulletDirection()*offsetFromObject;

	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
	}

	protected virtual void SetupBullet(GameObject bulletGO)
	{
		SetParentToBullet(bulletGO);
		SetMovementDirectionToBullet(bulletGO);
	}

	protected virtual Vector2 BulletDirection()
	{
		int x = animator.GetInteger("MovementX");
		int y = animator.GetInteger("MovementY");

		return new Vector2(x, y);
	}

	protected void SetParentToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out Bullet bs))
		{
			bs.SetParent(gameObject);
		}
	}

	protected void SetMovementDirectionToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out EntityMovement em))
		{
			em.CurrentMovementDirection = BulletDirection();
		}
	}
}