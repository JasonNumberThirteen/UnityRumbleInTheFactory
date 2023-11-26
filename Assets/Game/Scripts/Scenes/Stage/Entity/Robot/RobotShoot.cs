using UnityEngine;

public class RobotShoot : MonoBehaviour
{
	public GameObject bullet;
	[Min(0f)] public float offsetFromObject = 0.5f;

	protected Animator animator;

	private RobotAudioSource audioSource;

	public virtual void FireBullet()
	{
		audioSource.PlayShootSound();
		SetBullet(BulletInstance());
	}

	protected GameObject BulletInstance() => Instantiate(bullet, BulletPosition(), Quaternion.identity);
	protected Vector2 BulletPosition() => (Vector2)transform.position + BulletDirection()*offsetFromObject;

	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<RobotAudioSource>();
	}

	protected virtual void SetBullet(GameObject bullet)
	{
		SetParentToBullet(bullet);
		SetMovementDirectionToBullet(bullet);
	}

	protected virtual Vector2 BulletDirection()
	{
		int x = animator.GetInteger("MovementX");
		int y = animator.GetInteger("MovementY");

		return new Vector2(x, y);
	}

	protected void SetParentToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out BulletStats bs))
		{
			bs.parent = gameObject;
		}
	}

	protected void SetMovementDirectionToBullet(GameObject bullet)
	{
		if(bullet.TryGetComponent(out EntityMovement em))
		{
			em.Direction = BulletDirection();
		}
	}
}