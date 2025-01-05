using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class EntitySpawner : MonoBehaviour
{
	public UnityEvent entitySpawnedEvent;

	protected Timer timer;

	[SerializeField] private GameObject entityPrefab;

	private SpawnGameVisualEffect spawnGameVisualEffect;

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
			timer.onReset.AddListener(OnTimerReset);
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onReset.RemoveListener(OnTimerReset);
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	protected virtual void Awake()
	{
		timer = GetComponent<Timer>();
		spawnGameVisualEffect = GetComponentInChildren<SpawnGameVisualEffect>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnTimerReset()
	{
		SetSpawnVisualEffectActive(true);
	}

	private void OnTimerEnd()
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