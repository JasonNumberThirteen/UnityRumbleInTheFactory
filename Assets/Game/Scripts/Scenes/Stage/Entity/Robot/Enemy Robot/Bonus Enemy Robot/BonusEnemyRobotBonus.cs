using Random = UnityEngine.Random;
using UnityEngine;

public class BonusEnemyRobotBonus : MonoBehaviour
{
	public GameObject[] bonuses;

	public void SpawnBonus()
	{
		Instantiate(bonuses[BonusIndex()]);
		Destroy(this);
	}

	private int BonusIndex() => Random.Range(0, bonuses.Length);
}