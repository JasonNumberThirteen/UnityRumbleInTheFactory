using UnityEngine;

public class DestructionBonusEffect : BonusEffect
{
	public override void PerformEffect()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		foreach (GameObject enemy in enemies)
		{
			EntityExploder ee = enemy.GetComponent<EntityExploder>();

			if(ee != null)
			{
				ee.Explode();
			}
		}
	}
}