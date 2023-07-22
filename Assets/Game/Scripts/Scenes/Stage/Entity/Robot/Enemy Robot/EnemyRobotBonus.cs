using Random = UnityEngine.Random;
using UnityEngine;

public class EnemyRobotBonus : MonoBehaviour
{
	public GameObject[] bonuses;

	public void SpawnBonus()
	{
		int index = Random.Range(0, bonuses.Length);

		Instantiate(bonuses[index]);
		Destroy(this);
	}
}