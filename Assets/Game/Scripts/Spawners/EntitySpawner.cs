using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Timer))]
public class EntitySpawner : MonoBehaviour
{
	public UnityEvent entitySpawnedEvent;

	[SerializeField] private GameObject spawnVisualEffectGO;
	[SerializeField] private GameObject entityPrefab;

	private Timer timer;

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

	private void Awake()
	{
		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void OnTimerReset()
	{
		SetSpawnVisualEffectGOActive(true);
	}

	private void OnTimerEnd()
	{
		Spawn();
		SetSpawnVisualEffectGOActive(false);
	}

	private void Spawn()
	{
		var entityInstance = GetEntityInstance();

		if(entityInstance != null)
		{
			entitySpawnedEvent?.Invoke();
		}
	}

	private void SetSpawnVisualEffectGOActive(bool active)
	{
		if(spawnVisualEffectGO != null)
		{
			spawnVisualEffectGO.SetActive(active);
		}
	}
}