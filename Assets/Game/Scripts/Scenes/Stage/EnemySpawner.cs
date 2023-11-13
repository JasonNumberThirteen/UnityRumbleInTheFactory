using UnityEngine;

public class EnemySpawner : EntitySpawner
{
	public bool IsBonus {get; set;}
	
	public override void Spawn()
	{
		GameObject parent = GameObject.FindGameObjectWithTag(parentTag);

		if(parent != null)
		{
			GameObject instance = Instantiate(entity, gameObject.transform.position, Quaternion.identity);

			if(IsBonus)
			{
				if(instance.TryGetComponent(out RobotTrigger rt))
				{
					Destroy(rt);
				}

				instance.AddComponent<BonusEnemyRobotTrigger>();
				instance.AddComponent<BonusEnemyRobotColor>();
				instance.AddComponent<BonusEnemyRobotBonus>();
			}

			instance.transform.SetParent(parent.transform);
		}
	}
}