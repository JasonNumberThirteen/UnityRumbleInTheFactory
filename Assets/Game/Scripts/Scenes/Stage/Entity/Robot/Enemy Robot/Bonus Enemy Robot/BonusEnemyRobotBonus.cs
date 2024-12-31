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
		
		Instantiate(RandomBonus());
		Destroy(this);
	}

	private void Awake()
	{
		stageSoundManager = FindAnyObjectByType<StageSoundManager>();
	}

	private GameObject RandomBonus()
	{
		GameObject[] bonuses = StageManager.instance.enemySpawnManager.bonuses;
		int index = BonusIndex(bonuses);

		return bonuses[index];
	}

	private int BonusIndex(GameObject[] bonuses) => Random.Range(0, bonuses.Length);
}