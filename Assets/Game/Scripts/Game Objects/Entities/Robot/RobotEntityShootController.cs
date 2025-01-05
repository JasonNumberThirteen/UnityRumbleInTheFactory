using UnityEngine;

public class RobotEntityShootController : MonoBehaviour
{
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField, Min(0f)] private float offsetFromGO = 0.1f;

	protected StageSoundManager stageSoundManager;

	private RobotEntityAnimatorController robotEntityAnimatorController;

	public virtual void FireBullet()
	{
		SetupBullet(Instantiate(bulletPrefab, GetBulletPosition(), Quaternion.identity));
	}

	protected virtual void Awake()
	{
		robotEntityAnimatorController = GetComponent<RobotEntityAnimatorController>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
	}

	protected virtual void SetupBullet(GameObject bulletGO)
	{
		SetParentToBulletGOIfPossible(bulletGO);
		SetMovementDirectionToBulletGOIfPossible(bulletGO);
	}

	protected void SetParentToBulletGOIfPossible(GameObject bulletGO)
	{
		if(bulletGO.TryGetComponent(out BulletEntity bulletEntity))
		{
			bulletEntity.SetParent(gameObject);
		}
	}

	protected void SetMovementDirectionToBulletGOIfPossible(GameObject bulletGO)
	{
		if(bulletGO.TryGetComponent(out EntityMovementController entityMovementController))
		{
			entityMovementController.CurrentMovementDirection = robotEntityAnimatorController.GetMovementDirection();
		}
	}

	private Vector2 GetBulletPosition() => (Vector2)transform.position + robotEntityAnimatorController.GetMovementDirection()*offsetFromGO;
}