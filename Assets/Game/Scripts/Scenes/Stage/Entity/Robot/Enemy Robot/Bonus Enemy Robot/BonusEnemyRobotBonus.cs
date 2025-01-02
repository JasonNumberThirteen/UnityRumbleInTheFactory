using UnityEngine;

public class BonusEnemyRobotBonus : MonoBehaviour
{
	private BonusSpawnManager bonusSpawnManager;
	
	public void SpawnBonus()
	{
		if(bonusSpawnManager != null)
		{
			bonusSpawnManager.InstantiateRandomBonus();
		}
		
		Destroy(this);
	}

	private void Awake()
	{
		bonusSpawnManager = FindAnyObjectByType<BonusSpawnManager>();
	}
}