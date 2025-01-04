using UnityEngine;

[RequireComponent(typeof(Timer))]
public class FortressMetal : MonoBehaviour
{
	[SerializeField] private GameObject tileToSpawnPrefab;
	
	private Timer timer;

	private void Awake()
	{
		timer = GetComponent<Timer>();

		RegisterToListeners(true);
	}

	private void OnDestroy()
	{
		RegisterToListeners(false);
	}

	private void RegisterToListeners(bool register)
	{
		if(register)
		{
			timer.onEnd.AddListener(OnTimerEnd);
		}
		else
		{
			timer.onEnd.RemoveListener(OnTimerEnd);
		}
	}

	private void OnTimerEnd()
	{
		SpawnTile();
		Destroy(gameObject);
	}

	private void SpawnTile()
	{
		if(tileToSpawnPrefab != null)
		{
			Instantiate(tileToSpawnPrefab, transform.position, Quaternion.identity);
		}
	}
}