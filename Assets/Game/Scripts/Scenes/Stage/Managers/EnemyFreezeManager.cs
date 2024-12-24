using UnityEngine;

[RequireComponent(typeof(Timer))]
public class EnemyFreezeManager : MonoBehaviour
{
	public string enemyTag;

	private Timer timer;
	
	public void FreezeAllEnemies() => SetEnemiesFreeze(true);
	public void UnfreezeAllEnemies() => SetEnemiesFreeze(false);
	public bool EnemiesAreFrozen() => timer.Started;
	
	public void InitiateFreeze(float duration)
	{
		timer.duration = duration;

		timer.ResetTimer();
	}

	private void Awake()
	{
		timer = GetComponent<Timer>();
	}

	private void SetEnemiesFreeze(bool freeze)
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		foreach (GameObject enemy in enemies)
		{
			if(enemy.TryGetComponent(out EnemyRobotFreeze erf))
			{
				erf.SetFreezeState(freeze);
			}
		}
	}
}