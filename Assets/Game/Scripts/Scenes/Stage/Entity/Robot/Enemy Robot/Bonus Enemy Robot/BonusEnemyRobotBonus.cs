using Random = UnityEngine.Random;
using UnityEngine;

public class BonusEnemyRobotBonus : MonoBehaviour
{
	private StageSoundManager stageSoundManager;
	
	public void SpawnBonus()
	{
		if(stageSoundManager != null)
		{
			stageSoundManager.PlaySound(SoundEffectType.BonusSpawn);
		}
		
		Instantiate(GetRandomBonus());
		Destroy(this);
	}

	private void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}

	private GameObject GetRandomBonus()
	{
		var bonuses = StageManager.instance.enemySpawnManager.bonuses;
		var index = Random.Range(0, bonuses.Length);

		return bonuses[index];
	}
}