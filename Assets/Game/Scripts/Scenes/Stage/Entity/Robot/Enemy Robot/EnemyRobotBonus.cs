using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotBonus : MonoBehaviour
{
	public GameObject[] bonuses;

	public void SpawnBonus()
	{
		int index = Random.Range(0, bonuses.Length - 1);

		Instantiate(bonuses[index]);
		Destroy(this);
	}
}