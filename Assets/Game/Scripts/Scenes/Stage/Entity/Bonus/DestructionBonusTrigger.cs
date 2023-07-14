using UnityEngine;

public class DestructionBonusTrigger : BonusTrigger
{
	public override void TriggerEffect(GameObject sender)
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

		base.TriggerEffect(sender);
	}
}