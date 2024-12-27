using UnityEngine;

[RequireComponent(typeof(Timer))]
public class FortressMetal : MonoBehaviour
{
	[SerializeField] private GameObject bricksTilePrefab;
	
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
		SpawnBricksTile();
		Destroy(gameObject);
	}

	private void SpawnBricksTile()
	{
		if(bricksTilePrefab != null)
		{
			Instantiate(bricksTilePrefab, gameObject.transform.position, Quaternion.identity);
		}
	}
}