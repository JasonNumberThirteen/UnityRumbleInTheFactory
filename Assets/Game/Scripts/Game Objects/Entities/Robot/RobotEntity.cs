using UnityEngine;

[RequireComponent(typeof(EntityExploder), typeof(RobotEntityRankController))]
public abstract class RobotEntity : MonoBehaviour
{
	private EntityExploder entityExploder;
	private RobotEntityRankController robotEntityRankController;
	private RobotEntityShield robotEntityShield;
	
	public abstract bool IsFriendly();
	public abstract void OnLifeBonusCollected(int lives);

	protected abstract StageEventType GetStageEventTypeOnDestructionEvent();

	public void OnRankBonusCollected(int ranks)
	{
		robotEntityRankController.IncreaseRankBy(ranks);
	}

	public void ActivateShield(float duration)
	{
		if(robotEntityShield != null)
		{
			robotEntityShield.ActivateShield(duration);
		}
	}

	protected virtual void Awake()
	{
		entityExploder = GetComponent<EntityExploder>();
		robotEntityRankController = GetComponent<RobotEntityRankController>();
		robotEntityShield = GetComponentInChildren<RobotEntityShield>();

		RegisterToListeners(true);
	}

	protected virtual void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			entityExploder.entityWasDestroyedEvent.AddListener(OnEntityDestroyed);
		}
		else
		{
			entityExploder.entityWasDestroyedEvent.RemoveListener(OnEntityDestroyed);
		}
	}

	private void OnEntityDestroyed()
	{
		var stageEventsManager = ObjectMethods.FindComponentOfType<StageEventsManager>();
		
		if(stageEventsManager != null)
		{
			stageEventsManager.SendEvent(new GameObjectStageEvent(GetStageEventTypeOnDestructionEvent(), gameObject));
		}
	}
}