using UnityEngine;

[RequireComponent(typeof(RobotEntityAnimatorController), typeof(RobotEntityRankController))]
public abstract class RobotEntityShootController : MonoBehaviour
{
	[SerializeField] private BulletEntity bulletEntityPrefab;
	[SerializeField, Min(0f)] private float offsetFromGO = 0.1f;

	protected StageSoundManager stageSoundManager;

	private RobotEntityAnimatorController robotEntityAnimatorController;
	private RobotEntityRankController robotEntityRankController;

	private readonly BulletStats bulletStats = new();

	protected virtual void FireBullet()
	{
		if(bulletEntityPrefab == null)
		{
			return;
		}

		var instance = Instantiate(bulletEntityPrefab, GetBulletPosition(), Quaternion.identity);

		instance.Setup(bulletStats, gameObject, robotEntityAnimatorController.GetMovementDirection());
	}

	protected virtual void Awake()
	{
		robotEntityAnimatorController = GetComponent<RobotEntityAnimatorController>();
		robotEntityRankController = GetComponent<RobotEntityRankController>();
		stageSoundManager = ObjectMethods.FindComponentOfType<StageSoundManager>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			robotEntityRankController.rankWasChangedEvent.AddListener(OnRankWasChanged);
		}
		else
		{
			robotEntityRankController.rankWasChangedEvent.RemoveListener(OnRankWasChanged);
		}
	}

	protected virtual void OnRankWasChanged(RobotRank robotRank, bool setOnStart)
	{
		UpdateBulletStats(robotRank);
	}

	private void UpdateBulletStats(RobotRank robotRank)
	{
		if(robotRank != null)
		{
			bulletStats.SetValues(robotRank.GetDamage(), robotRank.GetBulletMovementSpeed(), robotRank.CanDestroyMetal());
		}
	}

	private Vector2 GetBulletPosition() => (Vector2)transform.position + robotEntityAnimatorController.GetMovementDirection()*offsetFromGO;
}