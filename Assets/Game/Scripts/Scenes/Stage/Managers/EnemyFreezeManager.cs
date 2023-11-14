using UnityEngine;

public class EnemyFreezeManager : MonoBehaviour
{
	public Timer freezeTimer;
	public string enemyTag;
	
	public void FreezeAllEnemies() => SetEnemiesFreeze(true);
	public void UnfreezeAllEnemies() => SetEnemiesFreeze(false);
	public bool EnemiesAreFrozen() => freezeTimer.Started;
	
	public void InitiateFreeze(float duration)
	{
		freezeTimer.duration = duration;

		freezeTimer.ResetTimer();
	}

	public void SetEnemiesFreeze(bool freeze)
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