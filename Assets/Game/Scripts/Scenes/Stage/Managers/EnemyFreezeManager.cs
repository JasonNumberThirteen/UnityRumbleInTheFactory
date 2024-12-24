using UnityEngine;

[RequireComponent(typeof(Timer))]
public class EnemyFreezeManager : MonoBehaviour
{
	public string enemyTag;

	private Timer freezeTimer;
	
	public void FreezeAllEnemies() => SetEnemiesFreeze(true);
	public void UnfreezeAllEnemies() => SetEnemiesFreeze(false);
	public bool EnemiesAreFrozen() => freezeTimer.Started;
	
	public void InitiateFreeze(float duration)
	{
		freezeTimer.duration = duration;

		freezeTimer.ResetTimer();
	}

	private void Awake()
	{
		freezeTimer = GetComponent<Timer>();
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