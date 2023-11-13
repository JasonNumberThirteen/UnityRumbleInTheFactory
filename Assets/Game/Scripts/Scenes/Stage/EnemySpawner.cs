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

				if(instance.TryGetComponent(out BonusEnemyRobotColor berc))
				{
					berc.targetColor = StageManager.instance.enemySpawnManager.bonusEnemyTargetColor;
					berc.fadeTime = StageManager.instance.enemySpawnManager.bonusEnemyColorFadeTime;
				}

				if(instance.TryGetComponent(out BonusEnemyRobotBonus berb))
				{
					berb.bonuses = StageManager.instance.enemySpawnManager.bonuses;
				}
			}

			instance.transform.SetParent(parent.transform);
		}
	}
}