using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class EntitySpawner : MonoBehaviour
{
	public UnityEvent<GameObject> entityWasSpawnedEvent;

	protected Timer timer;

	[SerializeField] private GameObject entityPrefab;

	private SpawnGameVisualEffect spawnGameVisualEffect;

	public void StartTimer()
	{
		timer.StartTimer();
	}

	public void SetEntityPrefab(GameObject entityPrefab)
	{
		this.entityPrefab = entityPrefab;
	}

	protected virtual GameObject GetEntityInstance() => entityPrefab != null ? Instantiate(entityPrefab, gameObject.transform.position, Quaternion.identity) : null;

	protected virtual void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.timerStartedEvent.AddListener(OnTimerStarted);
			timer.timerFinishedEvent.AddListener(OnTimerFinished);
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerFinishedEvent.RemoveListener(OnTimerFinished);
		}
	}

	protected virtual void Awake()
	{
		timer = GetComponent<Timer>();
		spawnGameVisualEffect = GetComponentInChildren<SpawnGameVisualEffect>(true);

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnTimerStarted()
	{
		SetSpawnVisualEffectActive(true);
	}

	private void OnTimerFinished()
	{
		Spawn();
		SetSpawnVisualEffectActive(false);
	}

	private void Spawn()
	{
		var entityInstance = GetEntityInstance();

		if(entityInstance != null)
		{
			entityWasSpawnedEvent?.Invoke(entityInstance);
		}
	}

	private void SetSpawnVisualEffectActive(bool active)
	{
		if(spawnGameVisualEffect != null)
		{
			spawnGameVisualEffect.SetActive(active);
		}
	}
}