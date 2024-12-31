using Random = UnityEngine.Random;
using UnityEngine;

public class BonusEnemyRobotBonus : MonoBehaviour
{
	public void SpawnBonus()
	{
		StageManager.instance.audioManager.PlaySound(SoundEffectType.BonusSpawn);
		Instantiate(RandomBonus());
		Destroy(this);
	}

	private GameObject RandomBonus()
	{
		GameObject[] bonuses = StageManager.instance.enemySpawnManager.bonuses;
		int index = BonusIndex(bonuses);

		return bonuses[index];
	}

	private int BonusIndex(GameObject[] bonuses) => Random.Range(0, bonuses.Length);
}