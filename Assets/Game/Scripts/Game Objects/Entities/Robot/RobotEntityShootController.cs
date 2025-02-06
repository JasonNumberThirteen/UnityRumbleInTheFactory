using UnityEngine;

public class RobotEntityShootController : MonoBehaviour
{
	[SerializeField] private BulletEntity bulletEntityPrefab;
	[SerializeField, Min(0f)] private float offsetFromGO = 0.1f;

	protected StageSoundManager stageSoundManager;

	private RobotEntityAnimatorController robotEntityAnimatorController;
	private RobotEntityRankController robotEntityRankController;
	private BulletStats bulletStats;

	public virtual void FireBullet()
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

	private void Start()
	{
		UpdateBulletStats(robotEntityRankController.GetCurrentRobotRank());
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			robotEntityRankController.rankChangedEvent.AddListener(OnRankChanged);
		}
		else
		{
			robotEntityRankController.rankChangedEvent.RemoveListener(OnRankChanged);
		}
	}

	protected virtual void OnRankChanged(RobotRank robotRank)
	{
		UpdateBulletStats(robotRank);
	}

	private void UpdateBulletStats(RobotRank robotRank)
	{
		bulletStats = new BulletStats(robotRank.GetDamage(), robotRank.GetBulletSpeed(), robotRank.CanDestroyMetal());
	}

	private Vector2 GetBulletPosition() => (Vector2)transform.position + robotEntityAnimatorController.GetMovementDirection()*offsetFromGO;
}