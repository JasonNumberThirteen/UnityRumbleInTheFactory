using UnityEngine;

public class RobotEntityShootController : MonoBehaviour
{
	[SerializeField] private BulletEntity bulletEntityPrefab;
	[SerializeField, Min(0f)] private float offsetFromGO = 0.1f;

	protected StageSoundManager stageSoundManager;

	private RobotEntityAnimatorController robotEntityAnimatorController;

	public virtual void FireBullet()
	{
		if(bulletEntityPrefab != null)
		{
			SetupBulletEntity(Instantiate(bulletEntityPrefab, GetBulletPosition(), Quaternion.identity));
		}
	}

	protected virtual void Awake()
	{
		robotEntityAnimatorController = GetComponent<RobotEntityAnimatorController>();
		stageSoundManager = FindAnyObjectByType<StageSoundManager>(FindObjectsInactive.Include);
	}

	protected virtual void SetupBulletEntity(BulletEntity bulletEntity)
	{
		bulletEntity.Setup(gameObject, robotEntityAnimatorController.GetMovementDirection());
	}

	private Vector2 GetBulletPosition() => (Vector2)transform.position + robotEntityAnimatorController.GetMovementDirection()*offsetFromGO;
}