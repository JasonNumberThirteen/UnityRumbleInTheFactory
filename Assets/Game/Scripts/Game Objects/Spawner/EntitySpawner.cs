using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class EntitySpawner : MonoBehaviour
{
	public UnityEvent entitySpawnedEvent;

	protected Timer timer;

	[SerializeField] private GameObject entityPrefab;

	private SpawnGameVisualEffect spawnGameVisualEffect;

	public void StartTimer()
	{
		timer.StartTimer();
	}

	public void ResetTimer()
	{
		timer.ResetTimer();
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
			timer.timerWasResetEvent.AddListener(OnTimerWasReset);
			timer.timerReachedEndEvent.AddListener(OnTimerReachedEnd);
		}
		else
		{
			timer.timerStartedEvent.RemoveListener(OnTimerStarted);
			timer.timerWasResetEvent.RemoveListener(OnTimerWasReset);
			timer.timerReachedEndEvent.RemoveListener(OnTimerReachedEnd);
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

	private void OnTimerWasReset()
	{
		SetSpawnVisualEffectActive(true);
	}

	private void OnTimerReachedEnd()
	{
		Spawn();
		SetSpawnVisualEffectActive(false);
	}

	private void Spawn()
	{
		var entityInstance = GetEntityInstance();

		if(entityInstance != null)
		{
			entitySpawnedEvent?.Invoke();
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